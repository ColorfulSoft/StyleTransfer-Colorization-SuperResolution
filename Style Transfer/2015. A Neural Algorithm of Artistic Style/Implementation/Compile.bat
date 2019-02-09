C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:exe -optimize -DEBUG -out:"BuildVGG.exe" -r:"System.dll" -r:"System.IO.dll" "BuildVGG.cs"

BuildVGG.exe

DEL /F /Q "BuildVGG.exe"

DEL /F /Q "BuildVGG.pdb"

MOVE /Y "Net.Model" "src\Resources\"

MD Release

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:winexe -optimize -unsafe -out:"Release\A Neural Algorithm of Artistic Style.exe" -r:"System.dll" -r:"System.Drawing.dll" -r:"System.Threading.dll" -r:"System.Threading.Tasks.dll" -r:"System.IO.dll" -r:"System.Windows.Forms.dll" -r:"System.Reflection.dll" -resource:"src\Resources\Net.Model" -resource:"src\Resources\Content.jpg" -resource:"src\Resources\Style.jpg" -resource:"src\Resources\MainIcon.jpg" "src\Interface\MainForm.cs" "src\Network\Layers\Conv2DLayer.cs" "src\Network\Layers\Layer.cs" "src\Network\Layers\MaxPool2DLayer.cs" "src\Network\Layers\ReLULayer.cs" "src\Network\Optimization\Losses.cs" "src\Network\Optimization\Optimizer.cs" "src\Network\Network.cs" "src\Utils\Math.cs" "src\Utils\Tensor.cs" "src\NeuralArt.cs" "src\Properties.cs" 
cmd.exe