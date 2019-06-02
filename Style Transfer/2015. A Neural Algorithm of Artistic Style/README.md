# A Neural Algorithm of Artistic Style
Стилизация изображений с помощью итеративного алгоритма.

# Разработчики метода: Leon A. Gatys, Alexander S. Ecker, Matthias Bethge
* Авторская реализация: https://github.com/leongatys/PytorchNeuralStyleTransfer
* Текст работы: https://arxiv.org/abs/1508.06576 (либо в папке Paper)

# Компиляция и запуск приложения
1. Зайти в папку Implementation
2. Запустить файл Compile.bat. Будет создана папка Release, а в ней файл A Neural Algorithm of Artistic Style.exe
3. Пройти в папку Release и запустить A Neural Algorithm of Artistic Style.exe

# Минимальные системные требования
* Windows XP и выше. Разрядность любая.
* 1 ГБ оперативной памяти.
* .NET Framework 4.0 и выше. Возможно, программа запустится и на .NET Framework 3.5.

# Предупреждение!
Данный алгоритм является итеративным, то есть работает по принципу, похожему на принцип обучения нейронной сети. Это означает, что код является крайне ресурсоёмким и его использование на устройствах с неисправным охлаждением может привести к выходу из строя CPU, оперативной памяти или других систем. *Ответственность за ущерб, связанный с использованием данного ПО лежит на пользователе!*

# Примеры работы:

* Оригинал

![Оригинал](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/Buisness.jpg)

* Starry Night

![Starry Night](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Starry.jpg)

* Composition VII

![Composition VII](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Composition.jpg)

* Wave

![Wave](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Wave.jpg)

* Seated Nude

![Seated Nude](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Seated_Nude.jpg)

* Scream

![Scream](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Scream.jpg)

* Wreck

![Wreck](https://github.com/ColorfulSoft/StyleTransfer-Colorization-SuperResolution/blob/master/Style%20Transfer/2015.%20A%20Neural%20Algorithm%20of%20Artistic%20Style/Examples/70_Buisness_Wreck.jpg)

Все изображения стилизованы за 70 итераций. На моём ноутбуке с Intel(R) Core(TM) i3-4030U CPU @ 1.90 GHz и 4 ГБ оперативной памяти, одна итерация в данном разрешении занимает примерно 200 секунд.
