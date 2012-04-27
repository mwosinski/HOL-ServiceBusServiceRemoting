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
Imports System.Windows.Forms
Imports Common.Clients
Imports Common.Contracts

Public Class Main

    Private client As PublicServiceClient

    Public Sub New()
        ' Tcp: All communication to the Service Bus is performed using outbound TCP connections.
        ' Http: All communication to Service Bus is performed using outbound HTTP connections.
        ' AutoDetect: The Service bus client automatically selects between TCP and HTTP connectivity.
        Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode = Microsoft.ServiceBus.ConnectivityMode.AutoDetect

        Me.client = New PublicServiceClient()
        Me.InitializeComponent()
    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor

        Try
            Me.LoadCustomersList()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub LoadCustomersList()
        Try
            Dim customers() As Customer = Me.client.ListCustomers()
            Me.customersListView.Items.Clear()

            For Each customer As Customer In customers
                Dim item = New ListViewItem(customer.Name)
                item.SubItems.Add(customer.City)
                item.SubItems.Add(customer.BankingEntity.ToString())
                item.SubItems.Add(customer.Id.ToString())

                Me.customersListView.Items.Add(item)
            Next customer
        Catch e1 As Exception
            Throw
        End Try
    End Sub

    Private Sub moveToBankingEntityButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moveToBankingEntityButton.Click
        Dim customersId = New List(Of Guid)()
        Me.Cursor = Cursors.WaitCursor

        Try
            For Each item As ListViewItem In Me.customersListView.CheckedItems
                Dim customerId = New Guid(item.SubItems(3).Text)
                customersId.Add(customerId)
            Next item

            If customersId.Count > 0 Then
                Me.client.MoveCustomersToBankingEntity(customersId.ToArray(), Common.Contracts.BankingEntity.FictionalRetail)
                Me.LoadCustomersList()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class