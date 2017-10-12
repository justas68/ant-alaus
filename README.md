# ALUS

Automatinė Linijų Užbrėžimų Sistema

# Informacija

Programa skirta atpažinti skysčio kiekį įvairių formų stiklinėse naudojant jau suprogramuotą nuotraukų atpažinimo biblioteką [OpenCV.NET](https://bitbucket.org/horizongir/opencv.net).

# Tikslas

Nautraukoje atpažinti stiklinės formą ir "užbrėžti" liniją atitinkančia jos formą, taip pat "užbrėžti" liniją, kuri nurodo stiklinės skysčio pilnumą.
Panaudojant abiejų linijų informaciją ir pritaikant savo matematikos žinias, turėtume sugebėti apskaičiuoti skyčio tūrį stiklinėje.

# Emgu instaliacija

Download the zip file from https://kent.dl.sourceforge.net/project/emgucv/emgucv/3.2/libemgucv-windesktop-3.2.0.2682.zip
Unzip the zip file in the root directory (ant-alaus) and then rename libemgucv-windesktop-3.2.0.2682 to libemgucv.
The same goes for newer versions or older versions (the part after -windesktop-3.2.0.2682).
Copy the .dll files bin x64/x86 based on your computer architecture and paste the files into your projects debug folder.
