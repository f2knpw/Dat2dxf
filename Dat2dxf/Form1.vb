Imports System.IO
Imports System.Globalization
Imports NetVector3 = netDxf.Vector3
Imports netDxf
Imports netDxf.Entities
Imports System.Linq

Public Class Form1
    Private m_DossierCible As String = ""

    ' --- BOUTON DE CONVERSION ET CHARGEMENT ---
    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        ' 1. Effacer la liste immédiatement
        lstLog.Items.Clear()

        ' 2. Effacer l'aperçu précédent
        If picPreview.Image IsNot Nothing Then
            picPreview.Image.Dispose()
            picPreview.Image = Nothing
        End If

        ' 3. Forcer la mise à jour visuelle avant d'ouvrir la boîte de dialogue
        lstLog.Refresh()
        picPreview.Refresh()

        ' 4. Ouvrir la sélection de dossier
        Dim fbd As New FolderBrowserDialog()
        If fbd.ShowDialog() = DialogResult.OK Then
            m_DossierCible = fbd.SelectedPath

            ' Chargement et conversion
            Dim files = Directory.EnumerateFiles(m_DossierCible, "*.*") _
                        .Where(Function(s) s.ToLower.EndsWith(".dat") Or s.ToLower.EndsWith(".dxf"))

            For Each filePath As String In files
                If filePath.ToLower.EndsWith(".dat") Then
                    Try
                        ConvertirFichier(filePath)
                    Catch : End Try
                End If
                lstLog.Items.Add(Path.GetFileName(filePath))
            Next

            ' Sélection automatique du premier profil
            If lstLog.Items.Count > 0 Then lstLog.SelectedIndex = 0
        End If
    End Sub

    ' --- LOGIQUE DE CONVERSION ---
    Private Sub ConvertirFichier(fullPath As String)
        Dim points As New List(Of NetVector3)()
        Dim culture As CultureInfo = CultureInfo.InvariantCulture

        For Each line As String In File.ReadLines(fullPath)
            Dim parts = line.Split(New Char() {" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)
            Dim x, y As Double
            If parts.Length >= 2 AndAlso Double.TryParse(parts(0), NumberStyles.Any, culture, x) AndAlso
                                       Double.TryParse(parts(1), NumberStyles.Any, culture, y) Then
                points.Add(New NetVector3(x * 100, y * 100, 0))
            End If
        Next

        If points.Count > 2 Then
            If Not (points.First().X = points.Last().X And points.First().Y = points.Last().Y) Then
                points.Add(points.First())
            End If
            Dim doc As New DxfDocument()
            doc.Entities.Add(New Spline(points))
            doc.Save(Path.ChangeExtension(fullPath, ".dxf"))
        End If
    End Sub

    ' --- GESTION DE L'AFFICHAGE ---
    Private Sub lstLog_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLog.SelectedIndexChanged
        If lstLog.SelectedItem Is Nothing Then Exit Sub

        Dim fileName As String = lstLog.SelectedItem.ToString()
        Dim baseName As String = Path.GetFileNameWithoutExtension(fileName)
        Dim extension As String = Path.GetExtension(fileName).ToLower()

        Dim datPath As String = Path.Combine(m_DossierCible, baseName & ".dat")
        Dim dxfPath As String = Path.Combine(m_DossierCible, baseName & ".dxf")

        ' Logique de sélection :
        If extension = ".dxf" Then
            ' Si on clique sur le DXF, on affiche les deux pour comparer
            DessinerAperçuDouble(datPath, dxfPath)
        Else
            ' Si on clique sur le DAT, on n'affiche que le bleu (DXF = "")
            DessinerAperçuDouble(datPath, "")
        End If
    End Sub

    Private Sub DessinerAperçuDouble(datPath As String, dxfPath As String)
        Dim bmp As New Bitmap(picPreview.Width, picPreview.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            g.Clear(Color.White)

            Dim margin As Single = 20.0F
            Dim scale As Single = CSng(picPreview.Width - (margin * 2))
            Dim centerY As Single = CSng(picPreview.Height / 2)

            ' Ligne de corde
            g.DrawLine(Pens.LightGray, margin, centerY, CSng(picPreview.Width - margin), centerY)

            ' 1. Dessin du DXF en ROUGE (Large)
            If File.Exists(dxfPath) Then
                Dim dxfPoints = LirePointsDxf(dxfPath)
                If dxfPoints.Count > 1 Then
                    Dim pts = dxfPoints.Select(Function(p) New PointF(margin + (p.X / 100.0F * scale), centerY - (p.Y / 100.0F * scale))).ToArray()
                    g.DrawLines(New Pen(Color.Red, 3), pts)
                End If
            End If

            ' 2. Dessin du DAT en BLEU (Fin)
            If File.Exists(datPath) Then
                Dim datPoints = LirePointsDat(datPath)
                If datPoints.Count > 1 Then
                    Dim pts = datPoints.Select(Function(p) New PointF(margin + (p.X * scale), centerY - (p.Y * scale))).ToArray()
                    g.DrawLines(New Pen(Color.Blue, 1), pts)
                End If
            End If
        End Using

        If picPreview.Image IsNot Nothing Then picPreview.Image.Dispose()
        picPreview.Image = bmp
    End Sub

    ' --- LECTURE DES POINTS ---
    Private Function LirePointsDat(path As String) As List(Of PointF)
        Dim pts As New List(Of PointF)()
        Dim culture As CultureInfo = CultureInfo.InvariantCulture
        For Each line In File.ReadAllLines(path)
            Dim parts = line.Split(New Char() {" "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)
            Dim x, y As Double
            If parts.Length >= 2 AndAlso Double.TryParse(parts(0), NumberStyles.Any, culture, x) AndAlso
                                       Double.TryParse(parts(1), NumberStyles.Any, culture, y) Then
                pts.Add(New PointF(CSng(x), CSng(y)))
            End If
        Next
        Return pts
    End Function

    Private Function LirePointsDxf(path As String) As List(Of PointF)
        Dim pts As New List(Of PointF)()
        Try
            Dim doc = DxfDocument.Load(path)
            ' On récupère les FitPoints de la première spline trouvée
            If doc.Entities.Splines.Count > 0 Then
                For Each v In doc.Entities.Splines.First().FitPoints
                    pts.Add(New PointF(CSng(v.X), CSng(v.Y)))
                Next
            End If
        Catch : End Try
        Return pts
    End Function
End Class
