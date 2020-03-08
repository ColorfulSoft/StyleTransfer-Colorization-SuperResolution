C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:exe -optimize -DEBUG -out:"BuildWeights.exe" -r:"System.dll" -r:"System.IO.dll" "BuildWeights.cs"

BuildWeights.exe

DEL /F /Q "BuildWeights.exe"

DEL /F /Q "BuildWeights.pdb"

MOVE "Colornet.HModel" "src\Resources\"

MD "Release\"

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:winexe -optimize -unsafe -out:"Release\Let there be Color!.exe" -r:"System.dll" -r:"System.Drawing.dll" -r:"System.Threading.dll" -r:"System.Threading.Tasks.dll" -r:"System.IO.dll" -r:"System.Windows.Forms.dll" -r:"System.Reflection.dll" -resource:"src\Resources\Colornet.HModel" -resource:"src\Resources\MainIcon.jpg" -resource:"src\Resources\Original.jpg" "src\Data\Data.cs" "src\Interface\MainForm.cs" "src\Layers\Conv2DLayer.cs" "src\Layers\JoinLayer.cs" "src\Layers\LinearLayer.cs" "src\Layers\ReLULayer.cs" "src\Layers\SigmoidLayer.cs" "src\Layers\Upsample2DLayer.cs" "src\Network\Colornet.cs" "src\Utils\Half2Float.cs" "src\Utils\IOConverters.cs" "src\Utils\Tensor.cs" "src\Program.cs"
cmd.exe