C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:exe -optimize -DEBUG -out:"BuildWeights.exe" -r:"System.dll" -r:"System.IO.dll" "BuildWeights.cs"

BuildWeights.exe

DEL /F /Q "BuildWeights.exe"

DEL /F /Q "BuildWeights.pdb"

RD /S /Q "Release"

MOVE /Y "decoder.model" "src\Resources\"

MOVE /Y "SANet.model" "src\Resources\"

MOVE /Y "vgg_normalised.model" "src\Resources\"

MD "Release\"

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:winexe -optimize -unsafe -out:"Release\Arbitrary Style Transfer with Style-Attentional Networks.exe" -r:"System.dll" -r:"System.Drawing.dll" -r:"System.Threading.dll" -r:"System.Threading.Tasks.dll" -r:"System.IO.dll" -r:"System.Windows.Forms.dll" -r:"System.Reflection.dll" -resource:"src\Resources\Content.jpg" -resource:"src\Resources\decoder.model" -resource:"src\Resources\MainIcon.jpg" -resource:"src\Resources\SANet.model" -resource:"src\Resources\Style.jpg" -resource:"src\Resources\vgg_normalised.model" "src\Data\DecoderData.cs" "src\Data\EncoderData.cs" "src\Data\SANetData.cs" "src\Interface\MainForm.cs" "src\Layers\Conv2DLayer.cs" "src\Layers\ElementwiseSumLayer.cs" "src\Layers\MaxPool2DLayer.cs" "src\Layers\NormLayer.cs" "src\Layers\ReLULayer.cs" "src\Layers\SoftmaxLayer.cs" "src\Layers\Upsample2DLayer.cs" "src\Network\Decoder.cs" "src\Network\Encoder.cs" "src\Network\SANet.cs" "src\Network\Stylize.cs" "src\Utils\IOConverters.cs" "src\Utils\Tensor.cs" "src\Program.cs" "src\Properties.cs"

cmd.exe