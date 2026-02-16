Imports System.Globalization
Imports System.IO


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form
    Private selectedFolderPath As String = ""

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        btnConvert = New Button()
        lstLog = New ListBox()
        picPreview = New PictureBox()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnConvert
        ' 
        btnConvert.Location = New Point(657, 14)
        btnConvert.Name = "btnConvert"
        btnConvert.Size = New Size(168, 63)
        btnConvert.TabIndex = 0
        btnConvert.Text = "open .dat files directory"
        btnConvert.UseVisualStyleBackColor = True
        ' 
        ' lstLog
        ' 
        lstLog.FormattingEnabled = True
        lstLog.Location = New Point(856, 14)
        lstLog.Name = "lstLog"
        lstLog.Size = New Size(238, 424)
        lstLog.TabIndex = 1
        ' 
        ' picPreview
        ' 
        picPreview.BackColor = SystemColors.ControlLightLight
        picPreview.Location = New Point(14, 101)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(811, 329)
        picPreview.TabIndex = 2
        picPreview.TabStop = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1117, 450)
        Controls.Add(picPreview)
        Controls.Add(lstLog)
        Controls.Add(btnConvert)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "dat to dxf"
        CType(picPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub


    Friend WithEvents btnConvert As Button
    Friend WithEvents lstLog As ListBox
    Friend WithEvents picPreview As PictureBox

End Class
