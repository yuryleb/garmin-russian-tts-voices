pushd "Pycckuu__Milena 1.30"
"C:\Program Files (x86)\GMapTool\gmt\gmt.exe" -j -v -a -b 1 -c 1.39,0,006-D3363-00 -m milena -o "..\..\dist\Pycckuu__Milena 1.30\Pycckuu__Milena.vpm" -q -x DLLINFO.TXT BRKINF16.HDR CLM_RUR.DAT DPS_RUR.DAT DPS_MIL.DAT PHM_MIL.DAT PP_RUR.DAT SDCT_RUR.DAT SF16_MIL.DAT TTN_RUR.DAT SYNM_F16.DAT UDCT_RUR.DAT DLLIMG.BIN DLLIMG.SIG IMGEND.TST
popd
pushd "..\dist\Pycckuu__Milena 1.30"
"C:\Program Files (x86)\GMapTool\gmt\gmt.exe" -i -v Pycckuu__Milena.vpm >Pycckuu__Milena.txt
popd
pause
