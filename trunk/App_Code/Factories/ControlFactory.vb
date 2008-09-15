Imports Microsoft.VisualBasic
Namespace FactoryLayer
    Public Class ControlFactory
        Private Shared controlFactory As ControlFactory
        Public Function GetInstance() As ControlFactory
            If controlFactory Is Nothing Then
                controlFactory = New ControlFactory
            End If
            Return controlFactory
        End Function
        Private Sub New()

        End Sub
        
    End Class
End Namespace
