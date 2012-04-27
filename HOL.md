﻿#Service Remoting With Windows Azure Service Bus#

## Overview ##

Due to recent bank regulation, the Fictional Bank is ordered to split into two separate banking entities: Fictional Investment and Fictional Retail. The IT department needs to restructure the existing customer relationship management (CRM) such that both banking entities can continue to share customer data even though they are no longer within the same corporate network boundary. The existing CRM Web services in Fictional Bank are largely written using Windows Communication Foundation and hosted on-premises.

Initially, the IT department decides to continue hosting the CRM Web services on-premises, at Fictional Investment. To achieve the goal of providing services to both entities, they choose to expose a subset of the CRM functionality through a second service, which they publish externally using the Windows Azure Service Bus, thus allowing clients at Fictional Retail access to the required functionality. This solution enables them to accomplish their objective with minimal changes to the service and client applications, which are mostly limited to WCF configuration changes.

In a second phase, Fictional Investment outsources its application services hosting. As a result, they relocate the service that they share with Fictional Retail to Windows Azure and host it in a worker role. Despite its new location, and because the Service Bus namespace allows the service to continue to be published at its present location, clients remain unaware of the change.

This hands-on lab walks you through this scenario using a sample application that replicates, albeit in a simplistic manner, the application architecture at Fictional Bank.

### Objectives ###

In this hands-on lab, you will learn how to:

- Provision Windows Azure projects and service namespaces

- Manage Access Control Service issuers and rules to provide service and client authentication

- Host services on-premises and publish then remotely using the Service Bus

- Publish services hosted in Windows Azure using the Service Bus

 
### Prerequisites ###

The following is required to complete this hands-on Lab:

- IIS 7 (with ASP.NET, WCF HTTP Activation)

- [Microsoft .NET Framework 4.0](http://go.microsoft.com/fwlink/?linkid=186916)

- [Microsoft Visual Studio 2010](http://msdn.microsoft.com/vstudio/products/)

- [Windows Azure Tools for Visual Studio 1.6](http://www.microsoft.com/windowsazure/sdk/)

- [Windows Azure  Libraries for .Net 1.6](http://www.microsoft.com/web/gallery/install.aspx?appid=WindowsAzureLibrariesNET)

 
You must have Internet access to complete the lab.

### Setup ###
In order to execute the exercises in this hands-on lab you need to set up your environment.

1. Open a Windows Explorer window and browse to the lab's **Source** folder.

1. Double-click the **Setup.cmd** file in this folder to launch the setup process that will configure your environment and install the Visual Studio code snippets for this lab.

1. If the User Account Control dialog is shown, confirm the action to proceed.

 
>**Note:** Make sure you have checked all the dependencies for this lab before running the setup.

### Using the Code Snippets ###

Throughout the lab document, you will be instructed to insert code blocks. For your convenience, most of that code is provided as Visual Studio Code Snippets, which you can use from within Visual Studio 2010 to avoid having to add it manually.

If you are not familiar with the Visual Studio Code Snippets, and want to learn how to use them, you can refer to the **Setup.docx** document in the **Assets** folder of the training kit, which contains a section describing how to use them.

 
### Exercises ###

This hands-on lab includes the following exercises:

1. Using the Service Bus to Host Services Remotely

1. Publishing Services Hosted in Windows Azure with the Service Bus

 
 
Estimated time to complete this lab: **45 minutes**.

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Every predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this lab describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in these procedures that you need to take into account.

<a name="Exercise1" />
##  Exercise 1: Using the Service Bus to Host Services Remotely  ##


In this exercise, you start with a solution that implements the Fictional Bank application architecture. The solution consists of two web services hosted by Fictional Investment. The first service, the CRM Data Service, provides application services for Fictional Investment. A second service, the FI Public Service, makes use of the first and exposes a subset of the CRM functionality to make it available to users in Fictional Retail.

 ![Application architecture with services and clients directly connected ](./images/Application-architecture-with-services-and-clients-directly-connected-.png?raw=true "Application architecture with services and clients directly connected ")        
    
  _Application architecture with services and clients directly connected_    

Initially, you run the service to reproduce a scenario where all services are hosted on-premises and communicate directly with their clients using a **NetTcpBinding** class. Such a scenario is feasible when all services reside within the same network and there are no intervening firewalls or Network Address Translation (NAT) devices along the path.

Nonetheless, Fictional Retail clients are not located within Fictional Investment's network.  In order to make the services externally accessible to Fictional Retail clients, you then update the WCF configuration to expose the FI Public Service over the Service Bus using a **NetTcpRelayBinding** class and a public endpoint address. This makes the service reachable from anywhere and allows you to fulfill one of the goals in the proposed scenario.

**Task 1 - Running the Service On-Premises**

In this task, you run the solution and test it locally using a **NetTcpBinding** to replicate the fully on-premises scenario, with services and clients located within the same network boundary.

1. Open Visual Studio from**Start | All Programs | Microsoft Visual Studio 2010**. 

1. In the **File** menu, choose **Open** **Project**. In the **Open Project** dialog, browse to **\Source\Ex1-ServiceBusRemoting\Begin**, select **ServiceRemoting.sln** in the folder for the language of your preference (Visual C# or Visual Basic) and click **Open**. 

	The solution contains the following projects:

	| **Common** | A library project, shared by all the projects in the solution, containing the service and data contracts as well as the client proxies for the Web services in the application |
	| --- | --- |
	| **FictionalInvestment.Crm.Data** | A console application that hosts the CRM application services for Fictional Bank |
	| **FictionalInvestment.PublicServices** | A console application that hosts the public service for Fictional Bank |
	| **FictionalRetail.Crm.Client** | A Windows Forms application that uses the public services provided by the CRM |

 	![Solution Explorer showing the CRM application components C](./images/Solution-Explorer-showing-the-CRM-application-components-C.png?raw=true "Solution Explorer showing the CRM application components C")  
 
	_Solution Explorer showing the CRM application components (C#)_  

 	![Solution Explorer showing the CRM application components VB](./images/Solution-Explorer-showing-the-CRM-application-components-VB.png?raw=true "Solution Explorer showing the CRM application components VB")
 
	_Solution Explorer showing the CRM application components (VB)_  

1. Configure the solution to launch the client and both the CRM Data Service and the FI Public Service simultaneously. To do this, in **Solution Explorer** right-click the **ServiceRemoting** solution and select **Set StartUp Projects**. In the **Solution 'ServiceRemoting' Property Pages** dialog, select the option labeled **Multiple startup projects**, and then set the **Action** for the **Fictional.Investment.Crm.Data**, **Fictional.Investment.PublicServices**, and **FictionalRetail.Crm.Client** projects to _**Start**._ Ensure that the order of the projects is as shown in the figure below. To change the starting order, select a project in the list and click the up or down arrow to move it. Press **OK** to confirm your changes.  

 	![Configuring the start up order of the projects in the solution](./images/Configuring-the-start-up-order-of-the-projects-in-the-solution.png?raw=true "Configuring the start up order of the projects in the solution")  
 
	_Configuring the start up order of the projects in the solution_  

1. Press **F5** to build the solution and start the services and the client application.

1. Notice that each service displays the URL where it listens and that both services, the CRM Data Service and the FI Public Service, are currently using net.tcp and listening at the loopback address (localhost).

1. Switch to the **Fictional Retail CRM Client** application. The UI presents a list of customers and their current assignment to one of the two banking entities. 

1. Move one or more customers from Fictional Investment to Fictional Retail by selecting the check box next to each customer's name and clicking **Move to Fictional Retail Bank**. Confirm that the operation succeeds by observing the status messages in the console windows of the CRM Data Service and the FI Public Service.  

 	![Testing the on-premises scenario with a NetTcpBinding](./images/Testing-the-on-premises-scenario-with-a-NetTcpBinding.png?raw=true "Testing the on-premises scenario with a NetTcpBinding")  
  
	_Testing the on-premises scenario with a NetTcpBinding_  

	> **Note:** The client application invokes the **ListCustomers** operation of the CRM service to retrieve the list of customers and the **MoveCustomersToBankingEntity** operation of the service to transfer the customers to another entity.

1. Press **ENTER** in both console windows to terminate the services and then exit the client application.

 
**Task 2 - Provisioning and Configuring the Service Bus Namespace** 

In this task, you will create a new Windows Azure Service Bus Namespace.

1. Navigate to the [Windows Azure portal](https://windows.azure.com/). You will be prompted for your Windows Live ID credentials if you are not already signed in.

1. Click **Service Bus, Access Control & Caching** link in the left pane, and then select the **Service Bus** item under the **Services** element.  

 	![Configuring Windows Azure Service bus](./images/Configuring-Windows-Azure-Service-bus.png?raw=true "Configuring Windows Azure Service bus")  
 
	_Configuring Windows Azure Service bus_  

1. Add a Service Namespace. A service namespace provides an application boundary for each application exposed through the Service Bus and is used to construct Service Bus endpoints for the application. To add a service namespace, click **New** button on the upper ribbon bar.  

 	![Creating a New Namespace](./images/Creating-a-New-Namespace.png?raw=true "Creating a New Namespace")  
 
	_Creating a New Namespace_  

1. Enter a name for your service**Namespace**, select a **Region** for your service to run in, choose the **Subscription** and click the **Create Namespace** button. Make sure to validate the availability of the name first. Service names must be globally unique as they are hosted in the cloud and accessible by whomever you decide to grant access.  

 	![Creating a new Service Namespace](./images/Creating-a-new-Service-Namespace.png?raw=true "Creating a new Service Namespace")  
 
	_Creating a new Service Namespace_  

	Please be patient while your service is activated. It can take a few minutes while it is provisioned.

	> **Note:** You may have to refresh the browser to show the service is active.

1. Once the namespace is active, click its name in the list of available namespaces to display the Service Namespace information page.  

 	![Summary page listing available service namespaces](./images/Summary-page-listing-available-service-namespaces.png?raw=true "Summary page listing available service namespaces")  
 
	_Summary page listing available service namespaces_  

1. In the **Properties** right pane, locate the **Service Bus** section and click the Default Key **View** button.  

 	![Namespace properties section](./images/Namespace-properties-section.png?raw=true "Namespace properties section")  
 
	_Namespace properties section_  

1. Record the value shown for **Default Issuer** and**Default Key,** and click **OK**. You will need these values later on to authenticate using the Access Control.  

 	![Service Bus default keys](./images/Service-Bus-default-keys.png?raw=true "Service Bus default keys")  
 
	_Service Bus default keys_  

 
You have now created a new namespace for this hands-on lab. To sign in at any time, simply navigate to the [Windows Azure portal](http://go.microsoft.com/fwlink/?LinkID=129428), click **Sign In** and provide your Live ID credentials.

**Task 3 - Configuring Access Control Service for Authentication**

The Windows Azure Access Control Service (ACS) service controls Service Bus authentication. You can take advantage of ACS to authenticate a host that listens on the Service Bus as well as clients that use the bus to connect to the service.

An issuer in Access Control Service represents a trusted application. Using ACS, you can create rules to map incoming claims, from trusted identity providers, into claims issued by ACS that an application or service consumes. More specifically, in the case of the Service Bus, these rules map the identity of the issuer into a series of claims that Service Bus uses to determine which actions the issuer is allowed to perform. For example, a_Listen_ claim issued by AC allows an application to expose services on the Service Bus, while a_Send_ claim allows it to send messages.

The Management Portal allows you to administer Access Control Service resourcesI, including its trusted issuers and the transformation rules. Additionally, a command line tool (sbaztool.exe) that you can use for managing Access Control service resources is included as part of the Service Bus samples.

In this task, you will use the Management Portal to create two issuers, one issuer for Fictional Investment and another one for Fictional Retail. Then, you will create rules to map the identity of the Fictional Investment issuer to the _Listen_ and _Send_ claims, allowing it to publish a service and send messages, and a second rule to map the identity of the Fictional Retail issuer to the _Send_ claim, so that it can connect and send messages to services published on the Service Bus.

>**Note:** For an alternative procedure that uses the command line tool to create issuers and Access Control Service rules, see Appendix 1 - Using the Windows Azure Access Control Management Command Line Tool.  
  

1. Navigate to the [Windows Azure portal](https://windows.azure.com/). You will be prompted for your Windows Live ID credentials if you are not already signed in.

1. Click **Service Bus, Access Control & Caching** link in the left pane, and then select the **Service Bus** item under the **Services** element.  

 	![Windows Azure ServiceBus namespaces](./images/Windows-Azure-ServiceBus-namespaces.png?raw=true "Windows Azure ServiceBus namespaces")  
 
	_Windows Azure ServiceBus namespaces_  

1. Select your Namespace and click the Access Control Service button in the upper ribbon.  

 	![Accessing Access Control Service](./images/Accessing-Access-Control-Service.png?raw=true "Accessing Access Control Service")  
 
	_Accessing Access Control Service_  

1. Once in the Access Control Service portal, select **Service Identities** from the left pane menu.  

 	![Access Control Service Portal](./images/Access-Control-Service-Portal.png?raw=true "Access Control Service Portal")  
 
	_Access Control Service Portal_  

1. Click the **Add** link to add a new identity.  

 	![Service Identities page](./images/Service-Identities-page.png?raw=true "Service Identities page")  
 
	_Service Identities page_  

1. In the **Add Service Identity** page, enter "fictionalInvestment" in the **Name** field under **Service Identity Settings.** Under **Credential Settings** pick "Symmetric Key" as type and click the **Generate** button to generate the symmetric key for this credential. Take note of this key, as you will need it in the next step. Finally, change the **Expiration Date** to "12/31/9999" and then click the **Save** button.

 	![Adding a Service Identity](./images/Adding-a-Service-Identity.png?raw=true "Adding a Service Identity")
 
	_Adding a Service Identity_

1. Now, click the **Add** Credential link to add a new credential to the "fictionalInvestment" identity.

 	![Edit Service Identity](./images/Edit-Service-Identity.png?raw=true "Edit Service Identity")
 
	_Edit Service Identity_

1. In the **Add Credential** page, pick "Password" as **Credential Type**. Then, in the **Password** field enter the symmetric key you generated in the previous step. Finally, change the **Expiration Date** to "12/31/9999" and click the **Save** button.      

 	![Add Credential](./images/Add-Credential.png?raw=true "Add Credential")    
 
	_Add Credential_    

1. In the **Edit Service Identity**, click the **Save** button to save all the changes you made so far.  

 	![Saving Service Identity information](./images/Saving-Service-Identity-information.png?raw=true "Saving Service Identity information")  
 
	_Saving Service Identity information_  

1. Now, create a new Identity called "fictionalRetail". To do so, repeat steps 5 through 9, but using "fictionalRetail" as the **Identity Name**.  

1. Now, you will create the rules that map the identity of the issuer into a series of claims that Service Bus uses to determine which actions the issuer can perform. To do so, click the **Rule groups** link in the left pane menu. You will first create the "Send" rule for the "fictionalInvestment" identity.  

 	![Adding Rule Groups](./images/Adding-Rule-Groups.png?raw=true "Adding Rule Groups")  
 
	_Adding Rule Groups_  

	>**Note:** A rule describes the logic executed when a request from a certain issuer to obtain a token for a certain resource is received. Given an incoming claim type and value, it specifies which claim type and value is included in the token that the Windows Azure AC issues in response. The value of the outgoing claim specifies whether the service allows access to the resource or action being requested (if access is denied, there will not be an outgoing claim).     
In this case, the rule maps the issuer ID for Fictional Investment into a _Listen_ Service Bus action.

1. In the **Rule Groups** page, click the **Default Rule Group for ServiceBus** group name under **Rule Groups** in order to edit it.  

 	![Editing the Default Rule Group for ServiceBus](./images/Editing-the-Default-Rule-Group-for-ServiceBus.png?raw=true "Editing the Default Rule Group for ServiceBus")  
 
	_Editing the Default Rule Group for ServiceBus_  

1. In the **Edit Rule Group** page, click **Add** to add a new rule.  

 	![Adding Editing Rule Group](./images/Adding-Editing-Rule-Group.png?raw=true "Adding Editing Rule Group")  
 
	_Adding Editing Rule Group_  

1. In the **Add Claim Rule** page, under **If** select "Access Controls Service" as **Input claim issuer**. Under **Input claim type** select "Select Type" and leave the default value for the combo box. Under **Input claim value** select **Enter value** and enter "fictionalInvestment". Under **Then** select **Enter type** as **Output claim type** and enter "net.windows.servicebus.action". Select **Enter value** as **Output claim value** and enter "Listen". Then click the **Save** button.  

 	![Adding a Listen Claim Rule](./images/Adding-a-Listen-Claim-Rule.png?raw=true "Adding a Listen Claim Rule")  
 
	_Adding a Listen Claim Rule_  

1. Now, you will add a "Send" rule to the fictionalInvestment identity. In the **Edit Rule Group** page, click **Add** to add a new rule. Then in the **Add Claim Rule** page, under **If** select "Access Controls Service" as **Input claim issuer**. Under **Input claim type** select "Select Type" and leave the default value for the combo box. Under **Input claim value** select **Enter value** and enter "fictionalInvestment". Under **Then** select **Enter type** as **Output claim type** and enter "net.windows.servicebus.action". Select **Enter value** as **Output claim value** and enter "Send". Then click the **Save** button.  

 	![Adding a Send Claim Rule](./images/Adding-a-Send-Claim-Rule.png?raw=true "Adding a Send Claim Rule")  
 
	_Adding a Send Claim Rule_  

	>**Note:** Granting _Send_ rights to the _fictionalInvestment_ issuer is in preparation for Exercise 2, when the FI Public Service is relocated to Windows Azure and the CRM Data Service is published on the Service Bus to allow the first service to access it.

1. Finally, add a "Send" rule _fictionalRetail_ issuer. In the **Edit Rule Group** page, click **Add** to add a new rule. Then in the **Add Claim Rule** page, under **If** select "Access Controls Service" as **Input claim issuer**. Under **Input claim type** select "Select Type" and leave the default value for the combo box. Under **Input claim value** select **Enter value** and enter "fictionalRetail". Under **Then** select **Enter type** as **Output claim type** and enter "net.windows.servicebus.action". Select **Enter value** as **Output claim value** and enter "Send". Then click the **Save** button.  

 	![Adding a Send Claim Rule](./images/Adding-a-Send-Claim-Rule.png?raw=true "Adding a Send Claim Rule")  
 
	_Adding a Send Claim Rule_  
    
	>**Note:** You have now created issuers for both the service and the client application and set up rules for the service issuer to grant it _Listen_ and _Send_ privileges and to grant _Send_ privileges for the client application.

1. In the **Edit Rule Group** page, click **Save** to save all the changes.  

 	![Saving Rule Group changes](./images/Saving-Rule-Group-changes.png?raw=true "Saving Rule Group changes")  
 
	_Saving Rule Group changes_  

 
**Task 4 - Configuring the Service to Listen on the Windows Azure Service Bus**

The FI Public Service registers its endpoint with the Service Bus, which exposes the service through specific, discoverable URIs and makes it available to anyone regardless of where they are located, even when the service sits behind a firewall.  
 ![Application architecture with services and clients connected via the Service Bus](./images/Application-architecture-with-services-and-clients-connected-via-the-Service-Bus.png?raw=true "Application architecture with services and clients connected via the Service Bus")  
 
_Application architecture with services and clients connected via the Service Bus_  

Publishing the FI Public Service on the Service Bus is very simple to do. You only need to add a reference to the Service Bus assembly, change the binding used by the service from **NetTcpBinding** to **NetTcpRelayBinding**, and update the endpoint address of the service to its new location in the cloud.

In this task, you update the Fictional Bank application to publish and consume the services it provides over the Service Bus.

1. If not already open, launch Visual Studio from**Start | All Programs | Microsoft Visual Studio 2010**.

1. In the **File** menu, choose **Open** **Project**. In the **Open Project** dialog, browse to **\Source\Ex1-ServiceBusRemoting\Begin**, select **Begin.sln** in the folder for the language of your preference (Visual C# or Visual Basic) and click **Open**.

1. Add a reference to the Service Bus assembly in the FI Public Service project. To do this, in **Solution Explorer**, right-click the **FictionalInvestment.PublicServices** project and select **Add Reference**.  In the**.NET** tab, select the **Microsoft.ServiceBus** assembly and click **OK**.

	> **Note:**  Verify you selected the version 1.6.0.0 of the **Microsoft.ServiceBus** assembly. If you cannot find the **Microsoft.ServiceBus** assembly in the **.NET** tab, use the **Browse** tab to locate this assembly inside the **%ProgramFiles%\Windows Azure SDK\v1.6\ServiceBus\ref\** folder.

 	![Locating the Microsoft.ServiceBus assembly in the SDK Assemblies folder](./images/Locating-the-Microsoft.ServiceBus-assembly-in-the-SDK-Assemblies-folder.png?raw=true "Locating the Microsoft.ServiceBus assembly in the SDK Assemblies folder")  
 
	_Locating the Microsoft.ServiceBus assembly in the SDK Assemblies folder_  

1. Open the **App.config** file of the **FictionalInvestment.PublicServices** project.

1. In the **services** section of **system.ServiceModel**, locate the **endpoint** element for the service named **FictionalInvestment.PublicServices.CrmPublicService**.

1. For this endpoint, update the value of the **address** attribute to _**sb://[YOUR-NAMESPACE].servicebus.windows.net/CrmPublicService**_, where [**YOUR_NAMESPACE**] is the Service Bus namespace that you defined for your project.

	>**Note:** The service namespace provides an application boundary for each application exposed through the Service Bus. You define namespaces at the Windows Azure portal.

1. Next, change the value of the **binding** attribute for this endpoint from **netTcpBinding** to **netTcpRelayBinding**.

1. Finally, add a new **behaviorConfiguration** attribute to the endpoint element and set its value to **serviceBusCredentialBehavior**. You will define this behavior in the next step. The updated endpoint element should appear as shown in the figure below, except for the namespace that should match your own.  

 	![Configuring the endpoint used to publish the service on the Service Bus](./images/Configuring-the-endpoint-used-to-publish-the-service-on-the-Service-Bus.png?raw=true "Configuring the endpoint used to publish the service on the Service Bus")  
 
	_Configuring the endpoint used to publish the service on the Service Bus_  

1. Inside **system.serviceModel**, provide the behavior configuration that you specified for the endpoint in the previous step. This behavior provides the Service Bus with the credentials required to authenticate the service publisher. To supply the credentials, insert a **behaviors** element as shown in the following (highlighted) configuration fragment. Replace the value of the **issuerSecret** attribute with the **Current Key** for the _fictionalInvestment_ issuer that you recorded in the previous task, when you created the issuer.  

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 CredentialsBehavior_)  

	````XML
	<configuration>
	  <system.serviceModel>
	    <services>
	      ...
	    </services>
	
	    <client>
	      ...
	    </client>
	    
	    <behaviors>
	      <endpointBehaviors>
	        <behavior name="serviceBusCredentialBehavior">
	          <transportClientEndpointBehavior credentialType="SharedSecret">
	            <clientCredentials>
	              <sharedSecret issuerName="fictionalInvestment" 
	                            issuerSecret="[YOUR_FI_ISSUER_KEY]"/>
	            </clientCredentials>
	          </transportClientEndpointBehavior>
	        </behavior>
	      </endpointBehaviors>
	    </behaviors>
	  </system.serviceModel>
	</configuration>
	````

1. Insert the following (highlighted) block into the **system.serviceModel** section to enable behavior and binding extensions.

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 BehaviorExtensions_)

	````XML
	<configuration>
	  ...
	  <system.serviceModel>
	    ...
	    <extensions>
	      <behaviorExtensions>
	        <add name="transportClientEndpointBehavior"
	             type="Microsoft.ServiceBus.Configuration.TransportClientEndpointBehaviorElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </behaviorExtensions>
	      <bindingExtensions>
	        <add name="netTcpRelayBinding"
	             type="Microsoft.ServiceBus.Configuration.NetTcpRelayBindingCollectionElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </bindingExtensions>
	    </extensions>
	  </system.serviceModel>
	</configuration>
	````

1. To complete the changes required to publish the service on the Service Bus, configure the connectivity mode of the service. Open the **Program.cs** file (for Visual C# projects) or **Module1.vb** file (for Visual Basic projects) in the **FictionalInvestment.PublicServices** project and insert the following (highlighted) code at the start of method **Main**.

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 ConnectivityMode - CS_)

	````C#
	internal static void Main()
	{
	  // Tcp: All communication to the Service Bus is performed using outbound TCP connections.
	  // Http: All communication to Service Bus is performed using outbound HTTP connections.
	  // AutoDetect: The Service bus client automatically selects between TCP and HTTP connectivity.
	  Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode =
	                             Microsoft.ServiceBus.ConnectivityMode.AutoDetect;
	
	  using (ServiceHost serviceHost = new ServiceHost(typeof(CrmPublicService)))
	  {
	    ...
	  }
	}
	````

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 ConnectivityMode - VB_)

	
	````VB.NET
Sub Main()
	  ' Tcp: All communication to the Service Bus is performed using outbound TCP connections.
	  ' Http: All communication to Service Bus is performed using outbound HTTP connections.
	  ' AutoDetect: The Service bus client automatically selects between TCP and HTTP connectivity.
	  Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode = Microsoft.ServiceBus.ConnectivityMode.AutoDetect
	
	  Using serviceHost As New ServiceHost(GetType(CrmPublicService))
	    serviceHost.Open()
	    ...
	  End Using
	End Sub
````

	

	>**Note:** The default connection mode between the listener service and Windows Azure Service Bus is TCP. However, if the network environment does not allow outbound TCP connections beyond HTTP, for example, because of changes in Fictional Investment's IT policy, you can configure the corresponding binding to use an HTTP connection to communicate with Service Bus. For most scenarios, it is recommended that you set the Mode to AutoDetect. This indicates that your application will attempt to use TCP to connect to the Service Bus, but will use HTTP if it is unable to do so.    
 
**Task 5 - Configuring the Client Application to Connect to a Service on the Windows Azure Service Bus**

In the previous task, you configured the service to listen on the Service Bus.  In this task, you set up the client application in a similar manner to allow it to connect to the service.

1. Add a reference to the Service Bus assembly in the client application project. To do this, in **Solution Explorer**, right-click the **FictionalRetail.Crm.Client** project and select **Add Reference**.  In the**.NET** tab, select the **Microsoft.ServiceBus** assembly and click **OK**.

	>**Note:**  Verify you selected the version 1.6.0.0 of the **Microsoft.ServiceBus** assembly. If you cannot find the **Microsoft.ServiceBus** assembly in the **.NET** tab, use the **Browse** tab to locate this assembly inside the **%ProgramFiles%\Windows Azure SDK\v1.6\ServiceBus\ref\**  folder.

1. Open the **App.config** file in the **FictionalRetail.Crm.Client** project.

1. Locate the single **endpoint** element in the **client** section of **system.serviceModel**.

1. Change the value of the **address** for the endpoint to _**sb://[YOUR-NAMESPACE].servicebus.windows.net/CrmPublicService**_, where [**YOUR_NAMESPACE**] is the Service Bus namespace that you defined for your project.

1. Next, change the value of the **binding** attribute from **netTcpBinding** to **netTcpRelayBinding**.

1. To complete the endpoint configuration, add a new **behaviorConfiguration** attribute to the endpoint element and set its value to **serviceBusCredentialBehavior**.

	Note that here you essentially apply the same changes to the client that you made to the service configuration. The updated service element should appear as shown in the figure below.  

 	![Configuring the client to consume a service on the Service Bus](./images/Configuring-the-client-to-consume-a-service-on-the-Service-Bus.png?raw=true "Configuring the client to consume a service on the Service Bus")  
 
	_Configuring the client to consume a service on the Service Bus_  

1. Finally, add a **behaviors** element to the **system.serviceModel** section, as shown below, to define the behavior configuration that you specified for the endpoint in the previous step. This behavior provides the Service Bus with the credentials required to authenticate the client application. Replace the value of the **issuerSecret** attribute with the **Current Key** for the _fictionalRetail_ issuer that you recorded in the previous task, when you created the issuer.   

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 CredentialsBehaviorClient_)  

	````XML
	<configuration>
	  <system.serviceModel>
	    <client>
	      ...
	    </client>
	    
	    <behaviors>
	      <endpointBehaviors>
	        <behavior name="serviceBusCredentialBehavior">
	          <transportClientEndpointBehavior credentialType="SharedSecret">
	            <clientCredentials>
	              <sharedSecret issuerName="fictionalRetail" 
	                            issuerSecret="[YOUR_FR_ISSUER_KEY]"/>
	            </clientCredentials>
	          </transportClientEndpointBehavior>
	        </behavior>
	      </endpointBehaviors>
	    </behaviors>
	  </system.serviceModel>
	</configuration>
	````

1. Insert the following (highlighted) block into the **system.serviceModel** section to enable behavior and binding extensions.

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 BehaviorExtensions_)

	````XML
	<configuration>
	  ...
	  <system.serviceModel>
	    ...
	    <extensions>
	      <behaviorExtensions>
	        <add name="transportClientEndpointBehavior"
	             type="Microsoft.ServiceBus.Configuration.TransportClientEndpointBehaviorElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </behaviorExtensions>
	      <bindingExtensions>
	        <add name="netTcpRelayBinding"
	             type="Microsoft.ServiceBus.Configuration.NetTcpRelayBindingCollectionElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </bindingExtensions>
	    </extensions>
	  </system.serviceModel>
	</configuration>
	````

1. Set the connectivity mode of the client application. Right-click the **Main.cs** file (for Visual C# projects) or **Main.vb** file (for Visual Basic projects) in the **FictionalRetail.Crm.Client** project and select **View Code**, then insert the following (highlighted) code at the start of the constructor for the **Main** class.

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 ConnectivityModeClient - CS_)

	````C#
	public Main()
	{
	  // Tcp: All communication to the Service Bus is performed using outbound TCP connections.
	  // Http: All communication to Service Bus is performed using outbound HTTP connections.
	  // AutoDetect: The Service bus client automatically selects between TCP and HTTP connectivity.
	  Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode =
	                             Microsoft.ServiceBus.ConnectivityMode.AutoDetect;
	    
	  this.client = new PublicServiceClient();
	  this.InitializeComponent();
	}
	````

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex01 ConnectivityModeClient - VB_)


	
	````VB.NET
	Public Sub New()
	  ' Tcp: All communication to the Service Bus is performed using outbound TCP connections.
	  ' Http: All communication to Service Bus is performed using outbound HTTP connections.
	  ' AutoDetect: The Service bus client automatically selects between TCP and HTTP connectivity.
	  Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode = Microsoft.ServiceBus.ConnectivityMode.AutoDetect
	
	  Me.client = New PublicServiceClient()
	  Me.InitializeComponent()
	End Sub
	````



 	
 
**Verification**

You are now ready to test the application using the Service Bus.

1. Configure the solution to launch both the CRM Data Service and the FI Public Service simultaneously. To do this, in **Solution Explorer**, right-click the **ServiceRemoting** solution and select **Set StartUp Projects**. In the **Solution 'ServiceRemoting' Property Pages** dialog, select the **Multiple startup projects** option, then set the **Action** for the **Fictional.Investment.Crm.Data** and **Fictional.Investment.PublicServices** projects to _Start_ and in that same starting order; set the remaining projects to _None_. To change the starting order, select a project in the list and click the up or down arrow to move it.

	>**Note:** Because the service may take slightly longer to start listening when hosted on the Service Bus, this time, you start the client application manually to ensure that the service has already started.

1. Press **F5** to build and run the application. This launches both the CRM Data Service and the FI Public Service.

	>**Note:** You may observe a Windows Firewall warning that it has blocked some features of the program and prompting to allow access.  Click **Cancel**. You do not need to enable any further access.

1. Wait for both services to start and show their status in their respective console windows. Notice the URL at which the FI Public Service is listening and how its scheme is now "_sb:_" and the URI contains your service namespace.

	>**Note:** Do not proceed with the next step until both services have started successfully. On startup, each service displays the URL where it is listening in its console window.

1. In **Solution Explorer**, right-click **FictionalRetail.Crm.Client**, point to **Debug** and select **Start new instance** to start the client application.

1. Notice that the client application presents the list of customers retrieved from the service showing that it successfully accessed the service published on the Service Bus.

1. Select one or more customers from the list and click **Move To Fictional Retail Bank**.  

 	![CRM application working against the service published by the Service Bus](./images/CRM-application-working-against-the-service-published-by-the-Service-Bus.png?raw=true "CRM application working against the service published by the Service Bus")  
 
	_CRM application working against the service published by the Service Bus_  

1. Notice that the client application continues to work as it did when the service was listening locally. This demonstrates that clients that are not located within the Financial Investment network are still able to connect to the service via the Service Bus.

1. Press **ENTER** in both console windows to terminate the services and then exit the client application.

 
### Exercise 2: Publishing Services Hosted in Windows Azure with the Service Bus ###

 Azure-hosted services can publish their endpoints using the Service Bus too.   
 ![Application architecture with services deployed to Windows Azure and listening on the Service Bus](./images/Application-architecture-with-services-deployed-to-Windows-Azure-and-listening-on-the-Service-Bus.png?raw=true "Application architecture with services deployed to Windows Azure and listening on the Service Bus")  
  
_Application architecture with services deployed to Windows Azure and listening on the Service Bus_  

In this exercise, you update the FI Public Service project and convert it into a worker role. This allows you to host the service in Windows Azure. Because the service is already listening via the Service Bus, clients remain unaware of the change and continue to run with no changes to their code or their configuration. Nevertheless, in this scenario, the CRM Data Service remains on-premises and you now need to configure it to listen on the Service Bus in order for the FI Public Service-now hosted in Windows Azure-to access it.

**Task 1 - Hosting the Service in a Windows Azure Worker Role**

In this task, you update the FI Public Service project, which is currently a Windows Console application, and convert it into a worker role.

1. Open Visual Studio in elevated administrator mode from**Start | All Programs | Microsoft Visual Studio 2010** by right clicking the **Microsoft Visual Studio 2010** shortcut and choosing **Run as administrator**. 

	>**Note:** Running in elevated administrator mode is required to run Windows Azure projects in the Compute Emulator.

1. In the **File** menu, choose **Open** **Project**. In the **Open Project** dialog, browse to **C:\WAPTK\Labs\** **ServiceRemotingVS2010\Source\Ex2-AzureServices\Begin**, select **ServiceRemoting.sln** in the folder for the language of your preference (Visual C# or Visual Basic) and click **Open**. Alternatively, you may continue with the solution that you obtained after completing the previous exercise.

1. Add a reference to the **Microsoft.ServiceBus** to the FI CrmData project. To do this, in **Solution Explorer**, right-click the **FictionalInvestment.Crm.Data** project and select **Add Reference**. In the **.NET** tab, select the **Microsoft.ServiceBus** reference and click **OK**.

1. In **Solution Explorer**, right-click the **FictionalInvestment.PublicServices** project and select **Add Reference**.  In the**.NET** tab, select the **Microsoft.WindowsAzure.Diagnostics**, **Microsoft.WindowsAzure.ServiceRuntime** and **Microsoft.WindowsAzure.StorageClient** components and click **OK**.  

 	![Adding a reference to the Windows Azure assemblies](./images/Adding-a-reference-to-the-Windows-Azure-assemblies.png?raw=true "Adding a reference to the Windows Azure assemblies")  
 
	_Adding a reference to the Windows Azure assemblies_  

1. Some assemblies required by the service are not ordinarily present in a Windows Azure VM; therefore, you must ship these assemblies with the service package to ensure that they are available. You do this by configuring the Copy Local property of the corresponding assembly reference.

	For Visual C# projects, to configure a reference, expand the **References** node for the **FictionalInvestment.PublicServices** project in **Solution Explorer**, right-click the corresponding reference in the **References** list, and select **Properties**.

	For Visual Basic projects, right-click the **FictionalInvestment.PublicServices** project in **Solution Explorer** and select **Properties**. In the **Project Properties** window, switch to the **References** tab, select the required assembly, and press **F4**.

	To add the assembly to the service package, in the **Properties** window of the assembly, change the value of the **Copy Local** setting to _True_.

	Use this procedure to include the **Microsoft.ServiceBus** assembly in the Windows Azure service package and ensure that both **Microsoft.WindowsAzure.Diagnostics** and **Microsoft.WindowsAzure.StorageClient** are deployed locally too.  

 	![Including an assembly in the Windows Azure service package](./images/Including-an-assembly-in-the-Windows-Azure-service-package.png?raw=true "Including an assembly in the Windows Azure service package")  
  
	_Including an assembly in the Windows Azure service package_  

1. Next, create a new cloud service project and add it to the solution. To do this, in the**File** menu, point to **Add** and then select **New Project**. In the **New Project** dialog, expand the language of your preference (Visual C# or Visual Basic) in the **Installed Templates** list and select **Cloud**. Choose the **Windows Azure Project** template, set the **Name** of the project to **CloudService** and accept the proposed location in the folder of the solution. Click **OK** to create the project.  

 	![Creating a new Windows Azure Cloud Service project](./images/Creating-a-new-Windows-Azure-Cloud-Service-project.png?raw=true "Creating a new Windows Azure Cloud Service project")  
 
	_Creating a new Windows Azure Cloud Service project_  

1. In the **New Windows Azure Project** dialog, click **OK** without adding any new roles. You will repurpose the existing service project and use it as a worker role.  

  	![No additional roles are required](./images/No-additional-roles-are-required.png?raw=true "No additional roles are required")  
 
	_No additional roles are required_  

1. Add the FI Public Service project as a worker role in the cloud service project. To do this, in **Solution Explorer**, right-click the **Roles** node in the**CloudService** project, point to **Add** and then select **Worker Role Project in Solution**. In the **Associate with Role Project** dialog, select the **FictionalInvestment.PublicServices** project and click **OK**.  

 	![Adding the FI Public Service project as a worker role](./images/Adding-the-FI-Public-Service-project-as-a-worker-role.png?raw=true "Adding the FI Public Service project as a worker role")  
 
	_Adding the FI Public Service project as a worker role_  

	> **Note:** The **FictionalInvestment.PublicServices** project is a standard Windows Console Application project. Ordinarily, you would not use this type of application as the starting point for a worker role. In order for Visual Studio to recognize it as a worker role candidate and allow its name to appear in the **Associate with Role Project** dialog, it was necessary to modify the project (.csproj for C# and .vbproj for Visual Basic) file to add a **RoleType** element and set its value to _Worker_. 

	![Roletype](images/Roletype.png?raw=true)
1. To be able to use the existing service project as a worker role, you need to include a role entry point in the project. To insert a pre-built entry point class, in **Solution Explorer**, right-click the **FictionalInvestment.PublicServices** project, point to **Add** and select **Existing Item**.  In the **Add** **Existing Item** dialog, navigate to **C:\WAPTK\Labs\** **ServiceRemotingVS2010\Source\Assets** and select the **WorkerRole.cs** file (for Visual C# projects) or **WorkerRole.vb** file (for Visual Basic projects) and click **Add**.  

 	![Solution Explorer showing the new worker role entry point class C](./images/Solution-Explorer-showing-the-new-worker-role-entry-point-class-C.png?raw=true "Solution Explorer showing the new worker role entry point class C")  
  
	_Solution Explorer showing the new worker role entry point class (C#)_  

 	![Solution Explorer showing the new worker role entry point class VB](./images/Solution-Explorer-showing-the-new-worker-role-entry-point-class-VB.png?raw=true "Solution Explorer showing the new worker role entry point class VB")  
  
	_Solution Explorer showing the new worker role entry point class (VB)_  

	>**Note:** The **WorkerRole** class is a **RoleEntryPoint** derived class modified to host the service. It contains methods that Windows Azure calls at various stages during the lifetime of the role.
        
	> Windows Azure invokes the **OnStart** method when the role starts. You can use this method to initialize the role. In the provided class, the **OnStart** method contains code to set up diagnostics settings that schedule an automatic transfer of the worker role logs to an Azure Storage account, where you can retrieve them. Note that the code initializes the Windows Azure Diagnostics configuration from a connection string in the service configuration file (**ServiceConfiguration.cscfg**), which is currently set to use Windows Azure storage emulator. If you deploy the service to Windows Azure, you need to update this configuration with your own Azure Storage account settings.    
      
	> The **Run** method of the **WorkerRole** class contains the code executed by the role to provide its functionality. In this case, it sets up a WCF **ServiceHost** for the FI Public Service and starts listening for requests on the Service Bus.  
Finally, Windows Azure calls the **OnStop** method just before it shuts down the worker role. Here, it is used to close the WCF service.

1. Open the **App.config** file in the **FictionalInvestment.PublicServices** project and replace the entry for the existing listener, named **configConsoleListener**, with the entry shown (highlighted) in the following configuration fragment.

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 AzureDiagnosticsConfig_)

	````XML
	<configuration>
	  ...
	  <system.diagnostics>
	    <trace>
	      <listeners>
	        <add type="Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener, Microsoft.WindowsAzure.Diagnostics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" name="AzureDiagnostics">
	          <filter type="" />
	        </add>
	      </listeners>
	    </trace>
	  </system.diagnostics>
	</configuration>
	````

	> **Note:** In order to provide diagnostics information, the service uses the **Trace** class in **System.Diagnostics** to write informational events and status messages to the log. Previously, the service used a **ConsoleTraceListener** to display this information in its console window. Now, when hosted in Windows Azure, the service needs to write this information to a place where you can view it. It uses a **TraceListener** specific to the Windows Azure environment that writes trace data to the Azure application logs.

1. Service  Bus requires Full Trust to run in Windows Azure. To enable full-trust, in **Solution Explorer**, expand the **Roles** node in the **CloudService** project and double-click the **FictionalInvestment.PublicServices** role. In the role properties page, select the **Configuration** tab and ensure that the **.NET trust level** is set to **Full Trust**.  

 	![Configuring the trust level of the worker role](./images/Configuring-the-trust-level-of-the-worker-role.png?raw=true "Configuring the trust level of the worker role")  
 
 	_Configuring the trust level of the worker role_  
 
**Task 2 - (Optional) Configuring the CRM Data Service to Listen on the Windows Azure Service Bus**

Because the FI Public Service now runs in Windows Azure and relies on the CRM Data Service, which continues to be hosted on-premises, it is necessary to update the latter to listen on the Service Bus.

> **Note:** The procedure required to do this is no different from the one that you performed in Exercise 1, when you published the FI Public Service itself via the Service Bus. If you started the current exercise from the solution that you obtained after completing Exercise 1, then, you need to complete this task; otherwise, if you used  the begin solution provided for the current exercise, then you can safely skip this task. All the necessary changes are already included in the begin solution.

> In either case, you **DO** still require to update the endpoint address and the credentials of the service in the **App.config** file of the **FictionalInvestment.Crm.Data** project, as well as the endpoint address and credentials of the client in the **App.config** file of the **FictionalInvestment.PublicServices** and **FictionalClient.Crm.Client** projects. This is simply because the values are specific to your Service Bus project. You will find instructions on how to do this in the **Verification** section of this exercise.

1. Open the **App.config** file of the **FictionalInvestment.Crm.Data** project.

1. In the **services** section of **system.ServiceModel**, locate the **endpoint** element for the service named **FictionalInvestment.Crm.Data.CrmDataService**. 

1. For this endpoint, update the value of the **address** attribute to _**sb://[YOUR-NAMESPACE].servicebus.windows.net/CrmDataService**_, where [**YOUR-NAMESPACE**] is the Service Bus namespace that you defined for your project.

1. The endpoint element should appear as shown in the figure below, except for the namespace that should match your own.  

	![Configuring-the-endpoint-used-to-publish-the-service-on-the-Service-Bus](images/Configuring-the-endpoint-used-to-publish-the-service-on-the-Service-Bus.png?raw=true)  
 _Configuring the endpoint used to publish the CRM Data Service on the Service Bus_  
1. Inside **system.serviceModel**, on the **behaviors** element, replace the value of the **issuerSecret** attribute with the issuer **key** for the _fictionalInvestment_ issuer that you obtained during the previous exercise.  

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 CredentialsBehaviorACS_)  

	````XML
	<configuration>
	  <system.serviceModel>
		...
		<behaviors>
		  <endpointBehaviors>
			<behavior name="serviceBusCredentialBehavior">
			  <transportClientEndpointBehavior credentialType="SharedSecret">
				<clientCredentials>
				  <sharedSecret issuerName="fictionalInvestment" 
								issuerSecret="[YOUR_FI_ISSUER_KEY]"/>
				</clientCredentials>
			  </transportClientEndpointBehavior>
			</behavior>
		  </endpointBehaviors>
		</behaviors>
	  </system.serviceModel>
	</configuration>
	````  



1. Insert the following (highlighted) block into the **system.serviceModel** section to enable behavior and binding extensions.  

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 CrmDataBehaviorExtensions_)  

	````XML
	<configuration>
	  ...
	  <system.serviceModel>
	    ...
	    <extensions>
	      <behaviorExtensions>
	        <add name="transportClientEndpointBehavior"
	             type="Microsoft.ServiceBus.Configuration.TransportClientEndpointBehaviorElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </behaviorExtensions>
	      <bindingExtensions>
	        <add name="netTcpRelayBinding"
	             type="Microsoft.ServiceBus.Configuration.NetTcpRelayBindingCollectionElement, Microsoft.ServiceBus, Version=1.6.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"/>
	      </bindingExtensions>
	    </extensions>
	  </system.serviceModel>
	</configuration>
	````  

1. The FI Public Services makes use of the CRM Data Service, which now requires publishing on the Service Bus too. To complete the preparations for the service, open the **App.config** file for the **FictionalInvestment.PublicServices** project and configure the existing **client** section in **system.serviceModel** substituting [YOUR-NAMESPACE] with the service namespace that you created for this project.  

	(Code Snippet - _Service Remoting with Service Bus Lab - Ex02 ClientEndpoint_)  

	````XML
	<configuration>
	  <system.serviceModel>
	    ...
	    <client>
	      <endpoint address="sb://[YOUR-NAMESPACE].servicebus.windows.net/CrmDataService"
	                binding="netTcpRelayBinding"
	                contract="Common.Contracts.ICrmDataService"
	                behaviorConfiguration="serviceBusCredentialBehavior"/>
	    </client>
	    ...
	  </system.serviceModel>
	</configuration>
 	````  
 
**Verification**

Before you begin, you need to ensure that you have configured the service endpoints and the issuer credentials correctly. Depending on how you arrived at the current solution, some of these values may already have been set. Note that this is necessary because these settings are specific to your Service Bus project. After that, you will run the Windows Azure project in the Compute Emulator and test it using the client application.

1. Open the **App.config** file of the **FictionalInvestment.PublicServices** project. Locate the single **endpoint** element in the **services** section of **system.ServiceModel** and replace the placeholder identified as [**YOUR_NAMESPACE**] in the **address** attribute of the endpoint with the Service Bus namespace that you defined for your project. 

1. Do the same for the existing endpoint defined in the **client** section of **system.ServiceModel**.

1. In the **behaviors** section, locate the **sharedSecret** element inside **transportClientEndpointBehavior** and update the **isssuerSecret** attribute with the issuer key for _fictionalInvestment_. Recall that you registered the keys for each issuer during Exercise 1.

1. Next, open the **App.config** file of the **FictionalInvestment.Crm.Data** project and repeat the previous steps to update the namespace for the endpoint in the **services** section and the credentials in the **behaviors** section using the same values.

1. Finally, open the **App.config** file for the **FictionalRetail.Crm.Client** project. Once again, update the namespace in the **client** section of **system.serviceModel** with your service namespace. Then, update the credentials in the **behaviors** section, but this time use the issuer key for _fictionalRetail_ instead.

1. You are now ready to test the solution. To launch the cloud service project in the Compute Emulator, in **Solution Explorer**, right-click **CloudService**, point to **Debug** and select **Start new instance**. The worker role starts, creates the service host, and starts listening at the Service Bus endpoint.

1. To verify that the service is running, right-click the Compute Emulator icon located in the system tray and select **Show Compute Emulator UI**.  

 	![Showing the compute emulator UI](./images/Showing-the-compute-emulator-UI.png?raw=true "Showing the compute emulator UI")  
 
	_Showing the compute emulator UI_  


1. In the **Service Deployments** tree view, expand the running deployment and select the **FictionalInvestment.PublicServices** node to show its diagnostics log.   

 	![FI Public Service hosted in the Compute Emulator](./images/FI-Public-Service-hosted-in-the-Compute-Emulator.png?raw=true "FI Public Service hosted in the Compute Emulator")  
 
	_FI Public Service hosted in the Compute Emulator_  

1. Next, start the CRM Data Service. To do this, in **Solution Explorer**, right-click the **FictionalInvestment.Crm.Data** project, point to **Debug** and select **Start new instance**. Wait for the service to start listening at its configured endpoint on the Service Bus. Notice the URL where the service is currently listening displayed in the console window.  

 	![CRM Data Service listening on the Service Bus](./images/CRM-Data-Service-listening-on-the-Service-Bus.png?raw=true "CRM Data Service listening on the Service Bus")  
 
	_CRM Data Service listening on the Service Bus_  

1. Now, launch the client application. Once again, right-click **FictionalRetail.Crm.Client** in **Solution Explorer**, point to **Debug** and select **Start new instance**. As you saw previously, the UI shows the list of customers and their current assigned entity, which confirms that the client is able to communicate with the service.  

1. Select one or more customers in the list and click **Move to Fictional Retail Bank**. Confirm that the call succeeds by observing the status messages in the console window of the CRM Data Service and the event log of the worker role in the Compute Emulator.  

 	![Using the FI Public Service hosted in the Compute Emulator](./images/Using-the-FI-Public-Service-hosted-in-the-Compute-Emulator.png?raw=true "Using the FI Public Service hosted in the Compute Emulator")  
 
 	_Using the FI Public Service hosted in the Compute Emulator_  
 
## Summary ##

By completing this hands-on lab, you saw how, with minimal changes to code and configuration, you can take an existing service and make it reachable from anywhere using the Service Bus. During the course of the lab, you learnt how to provision an Service Bus account and configure namespaces for you service. You took advantage of Windows Azure Access Control Service to provide claims-based authentication, creating rules that map an identity into claims that determine what actions an issuer is allowed to perform. Finally, you relocated a service to Windows Azure and saw how its clients are not affected by the change because the service continues to listen at the same URL, whether it is hosted on-premises or in the cloud.

## Appendix - Using the SBaZTool Command Line Tool ##

The following procedure describes the steps required to create issuers and rules using the SBaZTool Command Line Tool. For an alternative that uses the Access Control Service Management Portal, see **Task 3** in [Exercise 1: Using the Service Bus to Host Services Remotely](#Exercise1). Note that you do not need to complete this procedure if you have already created the issuers and set up Access Control Service rules using the Access Control Service Management Portal.

1. Download the SBaZTool from the [Windows Azure samples site](http://code.msdn.microsoft.com/windowsazure/site/search?f%5B0%5D.Type=Technology&f%5B0%5D.Value=Service%20Bus) and then unzip the content in any folder on your hard drive.

1. Open Visual Studio with elevated privileges and open the Authorization solution located at the C# folder. Select **Build** from the Visual Studio Menu and then **Rebuild Solution**.  

1. Open a command prompt window and change the current directory to the C# folder under the folder where you unzipped the samples. In order to create an issuer for the Fictional Investment Entity, enter the following command and then press **Enter**. Replace [YOUR-SB-NAMESPACE] with the name of your ServiceBus Namespace, and the [YOUR-SB-NAMESAPECE-DEFAULTKEY] with the Default Key of your ServiceBus Namespace. SBaZTool will return the key for the newly created issuer. Keep record of this value, as you will need it later.

	````CommandPrompt
sbaztool.exe -n [YOUR-SB-NAMESPACE] -k [YOUR-SB-NAMESPACE-DEFAULT-KEY] makeid FictionalInvestment
	````  
![Create-a-new-Issuer](images/Create-a-new-Issuer.png?raw=true)  
	_Create a new Issuer_  

1. Now Create an issuer for the Fictional Retail Entity by repeating the previous step but entering "fictionalRetail" as the issues name. Once again, SBaZTool will return the key for the newly created issuer. Keep record of this value, as you will need it later

1. Create a rule to grant _Listen_ permissions to the _fictionalInvestment_ issuer by executing the following command. Replace [YOUR-SB-NAMESPACE] with the name of your ServiceBus Namespace, and the [YOUR-SB-NAMESAPECE-DEFAULTKEY] with the Default Key of your ServiceBus Namespace.

	````CommandPrompt
	sbaztool.exe -n [YOUR-SB-NAMESPACE] -k [YOUR-SB-NAMESPACE-DEFAULT-KEY] grant Listen / fictionalInvestment
	````  

 	![Granting Listen permissions to the fictionalInvestemt issuer](./images/Granting-Listen-permissions-to-the-fictionalInvestemt-issuer.png?raw=true "Granting Listen permissions to the fictionalInvestemt issuer")  
 
	_Granting Listen permissions to the fictionalInvestemt issuer_  

1. Now, create a rule to grant _Send_ permissions to the _fictionalInvestment_ issuer by executing the following command. Replace [YOUR-SB-NAMESPACE] with the name of your ServiceBus Namespace, and the [YOUR-SB-NAMESAPECE-DEFAULTKEY] with the Default Key of your ServiceBus Namespace.  

	````CommandPrompt
	sbaztool.exe -n [YOUR-SB-NAMESPACE] -k [YOUR-SB-NAMESPACE-DEFAULT-KEY] grant Send / fictionalInvestment
	````  

 	![Granting Listen permissions to the fictionalInvestemt issuer](./images/image-0.png?raw=true "Granting Listen permissions to the fictionalInvestemt issuer")    
   
	_Granting Send permissions to the fictionalInvestment issuer_  

1. Now, create a rule to grant permissions to the _fictionalRetail_ issuer by executing the following command. Replace [YOUR-SB-NAMESPACE] with the name of your ServiceBus Namespace, and the [YOUR-SB-NAMESAPECE-DEFAULTKEY] with the Default Key of your ServiceBus Namespace.

	````CommandPrompt
	sbaztool.exe -n [YOUR-SB-NAMESPACE] -k [YOUR-SB-NAMESPACE-DEFAULT-KEY] grant Send / fictionalRetail
	````

 	![Granting Listen permissions to the fictionalRetail issuer](./images/Granting-Listen-permissions-to-the-fictionalRetail-issuer.png?raw=true "Granting Listen permissions to the fictionalRetail issuer")
 
	_Granting Listen permissions to the fictionalRetail issuer_

 
You have now created issuers for both the service and for the client application and set up rules for the service issuer to grant it _Listen_ and _Send_ privileges and for the client application permission to _Send_.







Page | 2 






