C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:exe -optimize -DEBUG -out:"BuildWeights.exe" -r:"System.dll" -r:"System.IO.dll" "BuildWeights.cs"

BuildWeights.exe

DEL /F /Q "BuildWeights.exe"

DEL /F /Q "BuildWeights.pdb"

RD /S /Q "src\Resources\Encoders"

RD /S /Q "src\Resources\Decoders"

RD /S /Q "Release"

MOVE /Y "Encoders" "src\Resources\"

MOVE /Y "Decoders" "src\Resources\"

MD "Release\"

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe -target:winexe -optimize -unsafe -out:"Release\Universal Style Transfer via Feature Transforms.exe" -r:"System.dll" -r:"System.Drawing.dll" -r:"System.Threading.dll" -r:"System.Threading.Tasks.dll" -r:"System.IO.dll" -r:"System.Windows.Forms.dll" -r:"System.Reflection.dll" -resource:"src\Resources\Decoders\feature_invertor_conv1_1.model" -resource:"src\Resources\Decoders\feature_invertor_conv2_1.model" -resource:"src\Resources\Decoders\feature_invertor_conv3_1.model" -resource:"src\Resources\Decoders\feature_invertor_conv4_1.model" -resource:"src\Resources\Decoders\feature_invertor_conv5_1.model" -resource:"src\Resources\Encoders\vgg_normalised_conv1_1.model" -resource:"src\Resources\Encoders\vgg_normalised_conv2_1.model" -resource:"src\Resources\Encoders\vgg_normalised_conv3_1.model" -resource:"src\Resources\Encoders\vgg_normalised_conv4_1.model" -resource:"src\Resources\Encoders\vgg_normalised_conv5_1.model" -resource:"src\Resources\Content.jpg" -resource:"src\Resources\MainIcon.jpg" -resource:"src\Resources\Style.jpg" "src\Data\DecoderData.cs" "src\Data\EncoderData.cs" "src\Interface\MainForm.cs" "src\Layers\AdaINLayer.cs" "src\Layers\Conv2DLayer.cs" "src\Layers\MaxPool2DLayer.cs" "src\Layers\ReLULayer.cs" "src\Layers\Upsample2DLayer.cs" "src\Layers\WCT.cs" "src\Network\Decoder.cs" "src\Network\Encoder.cs" "src\Network\Stylize.cs" "src\Utils\IOConverters.cs" "src\Utils\SVD.cs" "src\Utils\Tensor.cs" "src\Program.cs" "src\Properties.cs"

cmd.exe