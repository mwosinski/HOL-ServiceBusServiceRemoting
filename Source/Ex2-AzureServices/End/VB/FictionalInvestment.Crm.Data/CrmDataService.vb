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

Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Xml.Serialization
Imports Common.Contracts

Public Class CrmDataService
    Implements ICrmDataService

    Private ReadOnly xmlRepositoryPath As String
    Private customers As List(Of Customer)

    Public Sub New()
        Dim customersFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\Customers.xml")

        Me.xmlRepositoryPath = customersFilePath
        Me.Load()
    End Sub

    Public Function GetCustomer(ByVal id As Guid) As Customer Implements ICrmDataService.GetCustomer
        Return Me.customers.Where(Function(c) c.Id.Equals(id)).SingleOrDefault()
    End Function

    Public Function GetCustomers() As Customer() Implements ICrmDataService.GetCustomers
        Return Me.customers.OrderBy(Function(c) c.Name).ToArray()
    End Function

    Public Sub UpdateCustomer(ByVal customer As Customer) Implements ICrmDataService.UpdateCustomer
        Dim originalCustomer = Me.customers.Where(Function(c) c.Id.Equals(customer.Id)).SingleOrDefault()

        originalCustomer.Name = customer.Name
        originalCustomer.Address = customer.Address
        originalCustomer.City = customer.City
        originalCustomer.Zip = customer.Zip
        originalCustomer.State = customer.State
        originalCustomer.Country = customer.Country
        originalCustomer.Email = customer.Email
        originalCustomer.Phone = customer.Phone
        originalCustomer.BankingEntity = customer.BankingEntity

        Me.Persist()

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("[{0}] Updated info for customer: {1}", DateTime.Now, customer.Name)
        Console.ResetColor()
    End Sub

    Private Sub Load()
        Try
            Using reader = New StreamReader(Me.xmlRepositoryPath)
                Dim xmlSerializer = New XmlSerializer(GetType(Customer()))
                Dim deserializedCustomers = CType(xmlSerializer.Deserialize(reader), Customer())
                Me.customers = New List(Of Customer)(deserializedCustomers)
            End Using
        Catch e1 As FileNotFoundException
            Me.customers = New List(Of Customer)()
        Catch e2 As Exception
            Throw
        End Try
    End Sub

    Private Sub Persist()
        Using writer = New StreamWriter(Me.xmlRepositoryPath)
            Dim serial = New XmlSerializer(GetType(Customer()))
            serial.Serialize(writer, Me.customers.ToArray())
        End Using
    End Sub
End Class
