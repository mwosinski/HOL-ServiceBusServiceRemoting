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
Imports System.Runtime.Serialization

Namespace Contracts

    <DataContract()> _
    Public Class Customer
        Private privateId As Guid
        <DataMember()> _
        Public Property Id() As Guid
            Get
                Return privateId
            End Get
            Set(ByVal value As Guid)
                privateId = value
            End Set
        End Property

        Private privateName As String
        <DataMember()> _
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Set(ByVal value As String)
                privateName = value
            End Set
        End Property

        Private privateAddress As String
        <DataMember()> _
        Public Property Address() As String
            Get
                Return privateAddress
            End Get
            Set(ByVal value As String)
                privateAddress = value
            End Set
        End Property

        Private privateCity As String
        <DataMember()> _
        Public Property City() As String
            Get
                Return privateCity
            End Get
            Set(ByVal value As String)
                privateCity = value
            End Set
        End Property

        Private privateZip As String
        <DataMember()> _
        Public Property Zip() As String
            Get
                Return privateZip
            End Get
            Set(ByVal value As String)
                privateZip = value
            End Set
        End Property

        Private privateState As String
        <DataMember()> _
        Public Property State() As String
            Get
                Return privateState
            End Get
            Set(ByVal value As String)
                privateState = value
            End Set
        End Property

        Private privateCountry As String
        <DataMember()> _
        Public Property Country() As String
            Get
                Return privateCountry
            End Get
            Set(ByVal value As String)
                privateCountry = value
            End Set
        End Property

        Private privateEmail As String
        <DataMember()> _
        Public Property Email() As String
            Get
                Return privateEmail
            End Get
            Set(ByVal value As String)
                privateEmail = value
            End Set
        End Property

        Private privatePhone As String
        <DataMember()> _
        Public Property Phone() As String
            Get
                Return privatePhone
            End Get
            Set(ByVal value As String)
                privatePhone = value
            End Set
        End Property

        Private privateBankingEntity As BankingEntity
        <DataMember()> _
        Public Property BankingEntity() As BankingEntity
            Get
                Return privateBankingEntity
            End Get
            Set(ByVal value As BankingEntity)
                privateBankingEntity = value
            End Set
        End Property
    End Class
End Namespace
