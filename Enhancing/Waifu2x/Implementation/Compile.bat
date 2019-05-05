MD Release

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:winexe -optimize -unsafe -out:"Release\Waifu2x.exe" -r:"System.dll" -r:"System.Drawing.dll" -r:"System.Threading.dll" -r:"System.Threading.Tasks.dll" -r:"System.IO.dll" -r:"System.Windows.Forms.dll" -r:"System.Reflection.dll" -resource:"src\Resources\MainIcon.jpg" -resource:"src\Resources\Original.jpg" -resource:"src\Resources\Waifu2x.model" "src\Data\Data.cs" "src\Interface\MainForm.cs" "src\Layers\Conv2DLayer.cs" "src\Layers\LeakyReLULayer.cs" "src\Network\Waifu2x.cs" "src\Utils\IOConverters.cs" "src\Utils\Tensor.cs" "src\Program.cs"

cmd.exe