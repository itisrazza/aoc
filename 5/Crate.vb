Public Structure Crate
    Public ReadOnly Property Value() As Char

    Public Sub New(value As Char)
        Me.Value = value
    End Sub

    Public Overrides Function ToString() As String
        Return Value
    End Function
End Structure