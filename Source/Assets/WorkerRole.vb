' ----------------------------------------------------------------------------------
' Microsoft Developer & Platform Evangelism
' 
' Copyright (c) Microsoft Corporation. All rights reserved.
' 
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
' OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' ----------------------------------------------------------------------------------
' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
' ----------------------------------------------------------------------------------

Imports System.Net
Imports System.ServiceModel
Imports System.Threading
Imports Microsoft.ServiceBus
Imports Microsoft.WindowsAzure.Diagnostics
Imports Microsoft.WindowsAzure.ServiceRuntime

Public Class WorkerRole
    Inherits RoleEntryPoint

    Private ServiceHost As ServiceHost

    Public Overrides Sub Run()

        ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect

        Me.ServiceHost = New ServiceHost(GetType(CrmPublicService))
        Using Me.ServiceHost
            Me.ServiceHost.Open()

            Trace.TraceInformation("The worker role CrmPublicService is ready.")
            Trace.TraceInformation(String.Format("Listening at: {0}", Me.ServiceHost.Description.Endpoints(0).Address.Uri.AbsoluteUri))

            While (True)
                Thread.Sleep(30000)
                Trace.TraceInformation("Working...")
            End While
        End Using


    End Sub

    Public Overrides Function OnStart() As Boolean

        ' Set the maximum number of concurrent connections 
        ServicePointManager.DefaultConnectionLimit = 12

        ' Diagnostic configuration
        Dim config As DiagnosticMonitorConfiguration = DiagnosticMonitor.GetDefaultInitialConfiguration()
        config.Logs.ScheduledTransferPeriod = TimeSpan.FromSeconds(120)
        config.Logs.ScheduledTransferLogLevelFilter = LogLevel.Information
        DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config)

        ' For information on handling configuration changes
        ' see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

        Return MyBase.OnStart()

    End Function

    Public Overrides Sub OnStop()
        MyBase.OnStop()
        Me.ServiceHost.Close()
    End Sub
End Class
