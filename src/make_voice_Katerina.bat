pushd "Pycckuu__Katerina 1.30"
"C:\Program Files (x86)\GMapTool\gmt\gmt.exe" -j -v -a -b 2 -c 1.39,0,006-D1312-00 -m katerina -o "..\..\dist\Pycckuu__Katerina 1.30\Pycckuu__Katerina.vpm" -q -x DLLINFO.TXT KTRN_DAT.DAT KTRN_USL.DAT DICT.BDC DLLIMG.BIN DLLIMG.SIG IMGEND.TST
popd
pushd "..\dist\Pycckuu__Katerina 1.30"
"C:\Program Files (x86)\GMapTool\gmt\gmt.exe" -i -v Pycckuu__Katerina.vpm >Pycckuu__Katerina.txt
popd
pause
