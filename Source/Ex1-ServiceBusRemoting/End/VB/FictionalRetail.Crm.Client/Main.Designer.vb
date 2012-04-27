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

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.customersListView = New System.Windows.Forms.ListView
        Me.name1 = New System.Windows.Forms.ColumnHeader
        Me.city = New System.Windows.Forms.ColumnHeader
        Me.bankingEntity = New System.Windows.Forms.ColumnHeader
        Me.id = New System.Windows.Forms.ColumnHeader
        Me.moveToBankingEntityButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'customersListView
        '
        Me.customersListView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.customersListView.CheckBoxes = True
        Me.customersListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.name1, Me.city, Me.bankingEntity, Me.id})
        Me.customersListView.Location = New System.Drawing.Point(12, 12)
        Me.customersListView.Name = "customersListView"
        Me.customersListView.Size = New System.Drawing.Size(304, 166)
        Me.customersListView.TabIndex = 0
        Me.customersListView.UseCompatibleStateImageBehavior = False
        Me.customersListView.View = System.Windows.Forms.View.Details
        '
        'name1
        '
        Me.name1.Text = "Name"
        Me.name1.Width = 85
        '
        'city
        '
        Me.city.Text = "City"
        Me.city.Width = 70
        '
        'bankingEntity
        '
        Me.bankingEntity.DisplayIndex = 3
        Me.bankingEntity.Text = "Banking Entity"
        Me.bankingEntity.Width = 110
        '
        'id
        '
        Me.id.DisplayIndex = 2
        Me.id.Text = "Id"
        Me.id.Width = 0
        '
        'moveToBankingEntityButton
        '
        Me.moveToBankingEntityButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.moveToBankingEntityButton.Location = New System.Drawing.Point(106, 184)
        Me.moveToBankingEntityButton.Name = "moveToBankingEntityButton"
        Me.moveToBankingEntityButton.Size = New System.Drawing.Size(210, 23)
        Me.moveToBankingEntityButton.TabIndex = 1
        Me.moveToBankingEntityButton.Text = "Move to Fictional Retail Bank"
        Me.moveToBankingEntityButton.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 213)
        Me.Controls.Add(Me.moveToBankingEntityButton)
        Me.Controls.Add(Me.customersListView)
        Me.Name = "Main"
        Me.Text = "Fictional Retail CRM Client"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents customersListView As System.Windows.Forms.ListView
    Friend WithEvents name1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents city As System.Windows.Forms.ColumnHeader
    Friend WithEvents id As System.Windows.Forms.ColumnHeader
    Friend WithEvents moveToBankingEntityButton As System.Windows.Forms.Button
    Friend WithEvents bankingEntity As System.Windows.Forms.ColumnHeader
End Class
