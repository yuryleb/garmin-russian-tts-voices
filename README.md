# Дополнения и исправления для русских TTS-голосов из навигаторов Garmin

Начиная с 2009 года Garmin поставляет русские TTS-голоса, произносящие названия улиц
в голосовых подсказках навигатора на основе синтеза речи.
В силу сложности русского языка вообще и русских топонимических названий в частности,
все эти голоса озвучивают подсказки с большим количеством неверных ударений
и другими искажениями.
Ошибки произношения могут быть скорректированы словарями, встроенными в TTS-голоса,
но в данных голосах эти словари либо пусты, либо содержат не более десятка названий.

Большинство TTS-голосов уже сняты с официальной поддержки и исправляться не будут.
К сожалению, Garmin практически не реагирует на замечания/пожелания и по последним
версиям голосов, так что единственное, что остается - пытаться помочь себе самостоятельно.

К счастью, все файлы TTS-голосов `.vpm` для Garmin представляют собой тот же файловый
контейнер FAT, что и файлы его карт `.img` и успешно разбираются/собираются той же утилитой
[GMapTool](http://www.gmaptool.eu/en/content/gmaptool) в режиме *as subfiles*.

И при наличии специальных знаний вполне можно самостоятельно разобраться с содержимым
голоса, пополнить его словарь и даже расширить его внутреннюю обработку фраз перед
произношением такой полезной функциональностью, как:
* Склонение названий улиц в зависимости от контекста полной фразы.
* Распознавание и разделение на слова/фрагменты названий и индексов трасс
  *("Поверните на Р23" без исправлений звучит как "Поверните на разъезд двадцать третий")*
* Исправление полных фраз или их фрагментов для более благозвучного звучания
  *("Предупреждение об узких полосах" -> "Предупреждение о сужении дороги")*
* Восстановление окончаний для числительных *("2-Я" или "1-Го")*
* Обработка дат в названиях для произношения их в нужном падеже
  *("25 Октября Проспект" -> "проспект 20 пятого октября")*
* Перевод иностранных статусных названий дорог *(улица, проспект, шоссе и т.п.)*.
  На настоящий момент полный перевод сделан для названий из Эстонии, Латвии, Литвы, Венгрии, Франции,
  Испании и Италии.
  Для финских и немецких названий, написанных слитно со статусом *(Schützenstraße, Lappeenrannantie)*,
  такие "окончания" отделяются и произносятся заготовленными фонемами, близкими к нужному произношению.

К сожалению, такая функциональность доступна только для самого последнего поколения голосов `TTS3`, в
предыдущих можно только заменить словарь.

На сегодня существуют уже 3 "поколения" TTS-голосов, не совместимые между собой -
**заменить в навигаторе поставленный TTS-голос одного поколения на голос другого поколения нельзя**.

## Катерина 1.30 (TTS1)
Первый русский TTS-голос. Официальная поддержка прекращена.
* Первоисточник: RealSpeak/ScanSoft/Nuance Katerina
* Совместимые навигаторы: nuvi 13xx/14xx, 2455, 3790 ...
* Голосовая модель: Compact (bet2 16kHz)
* Встроенный словарь: [есть](src/Pycckuu__Katerina%201.30/DICT.BDC),
  бинарный формат BDC (Binary Native Platform Dictionary),
  максимальное число слов - 10000
* Компилятор словаря: редактор [RSUDE 2.1](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=16267391)
* Предобработка фраз: отсутствует

***

* Готовый голос: [Pycckuu__Katerina.vpm](dist/Pycckuu__Katerina%201.30/Pycckuu__Katerina.vpm)
* Исходные файлы голоса: [Pycckuu__Katerina 1.30](src/Pycckuu__Katerina%201.30/)
* Исходный словарь: [dictionary_Katerina.dct](src/dictionary_Katerina.dct),
  текстовый формат DCT в кодировке UTF-8 с BOM
* Компиляция словаря: предварительно установленым редактором RSUDE
  (корректно работает только на русской версии Windows XP)
* Сборка голоса: [make_voice_Katerina.bat](src/make_voice_Katerina.bat)


## Милена 1.30 (TTS2)
Новый русский TTS-голос следующего поколения. Официальная поддержка прекращена.
* Первоисточник: Nuance Vocalizer Milena
* Совместимые навигаторы: nuvi 30/40/42/50/52
* Голосовая модель: [Compact (bet2 16kHz)](src/Pycckuu__Milena%201.30/BRKINF16.HDR)
* Встроенный словарь: [есть](src/Pycckuu__Milena%201.30/UDCT_RUR.DAT),
  бинарный формат DAT
* Компилятор словаря: [Nuance Vocalizer for Automotive 5.3 + VoCon 3200 Embedded Development System 3.3 SDK](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=19069591)
* Предобработка фраз: отсутствует

***

* Готовый голос: [Pycckuu__Milena.vpm](dist/Pycckuu__Milena%201.30/Pycckuu__Milena.vpm)
* Исходные файлы голоса: [Pycckuu__Milena 1.30](src/Pycckuu__Milena%201.30/)
* Исходный словарь: [dictionary_Milena130.dct](src/dictionary_Milena130.dct),
  текстовый формат DCT в кодировке UTF-8 с BOM
* Компиляция словаря: [build_dictionary_Milena130.bat](src/build_dictionary_Milena130.bat),
  запускающий скопилированный [Python](https://www.python.org/download/releases/2.5.4/)-скрипт `dictcpl.pyc`.
* Сборка голоса: [make_voice_Milena130.bat](src/make_voice_Milena130.bat)


## Милена 2.10 (TTS3)
Кардинальное обновление Милены первой версии (Real Directions или Natural Guidance).
Официальная поддержка прекращена.
* Первоисточник: Nuance Vocalizer Milena
* Совместимые навигаторы: nuvi 2x97/3x97, ... , nuviCam, вся серия Drive
* Голосовая модель: [Standard (dri40_155mrf22 22kHz) + Compact (bet2 16kHz)](src/Pycckuu__Milena%202.10/BRKINF22.HDR)
* Встроенный словарь: [есть](src/Pycckuu__Milena%202.10/UDCT_RUR.DAT),
  бинарный формат DAT
* Компилятор словаря: [Nuance Vocalizer for Automotive 5.3 + VoCon 3200 Embedded Development System 3.3 SDK](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=19069591)
* Предобработка фраз: 
  * Предварительная замена слов: [RU.DCT](src/Pycckuu__Milena%202.10/RU.DCT),
    текст в кодировке UTF-8 **без BOM**
  * Постобработка/замена по умолчанию(?): [DFLT.DCT](src/Pycckuu__Milena%202.10/DFLT.DCT),
    текст в кодировке UTF-8 **без BOM**
  * Регулярные выражения замен: [RULESET.TXT](src/Pycckuu__Milena%202.10/RULESET.TXT),
    текст в кодировке UTF-8 с BOM, первая пустая строка обязательна
  * Дополнительный файл регулярных выражений замен: [RP_RSET.TXT](src/Pycckuu__Milena%202.10/RP_RSET.TXT),
    текст в кодировке UTF-8 с BOM, первая пустая строка обязательна, выполняется перед RULESET.TXT

***

* Готовый голос: [Pycckuu__Milena.vpm](dist/Pycckuu__Milena%202.10/Pycckuu__Milena.vpm)
* Исходные файлы голоса: [Pycckuu__Milena 2.10](src/Pycckuu__Milena%202.10/)
* Исходный словари:
  * Полный финальный словарь: [dictionary.dct](src/dictionary.dct),
    текстовый формат DCT в кодировке UTF-8 с BOM
  * Черновой словарь для русских названий: [dictionary.voc](src/dictionary.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Черновой словарь для названий городов: [cities.voc](src/cities.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Черновой словарь для иностранных названий: [dictionary_foreign.voc](src/dictionary_foreign.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Чистовой словарь для иностранных названий с восстановленными европейскими символами:
    [dictionary_foreign.dct](src/dictionary_foreign.dct),
    текстовый формат DCT в кодировке UTF-8 с BOM
* Компиляция словаря: [build_dictionary.bat](src/build_dictionary.bat),
  запускающий скопилированный [Python](https://www.python.org/download/releases/2.5.4/)-скрипт `dictcpl.pyc`
* Сборка голоса: [make_voice_Milena.bat](src/make_voice_Milena.bat)


## Татьяна 2.30 (TTS3)
Новый TTS-голос, но внутри та же Милена 2.10, расширенная поддержкой техники генерации речи CLC вместо
"устаревшей" G2P, благодаря чему основные фрагменты фраз звучат как записанный голос живого диктора,
с более медленным и даже эмоциональным произношением.
Причем база CLC не включена в файл голоса, она приходит отдельно вместе с каждым обновлением основной
"встроенной" карты навигатора.
Также голос дополнен несколькими новыми фразами для подсказок Real Directions.
Официальная поддержка как бы продолжается.
* Первоисточник: Nuance Vocalizer Milena
* Совместимые навигаторы: nuvi 2x97/3x97, ... , nuviCam, вся серия Drive
* Голосовая модель: [Standard (dri40_155mrf22 22kHz) + Compact (bet2 16kHz)](src/Pycckuu__Tatiana%202.30/BRKINF22.HDR)
* Встроенный словарь: [есть](src/Pycckuu__Tatiana%202.30/UDCT_RUR.DAT),
  бинарный формат DAT
* Компилятор словаря: [Nuance Vocalizer for Automotive 5.3 + VoCon 3200 Embedded Development System 3.3 SDK](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=19069591)
* Предобработка фраз: 
  * Предварительная замена слов: [RU.DCT](src/Pycckuu__Tatiana%202.30/RU.DCT),
    текст в кодировке UTF-8 **без BOM**
  * Постобработка/замена по умолчанию(?): [DFLT.DCT](src/Pycckuu__Tatiana%202.30/DFLT.DCT),
    текст в кодировке UTF-8 **без BOM**
  * Регулярные выражения замен: [RULESET.TXT](src/Pycckuu__Tatiana%202.30/RULESET.TXT),
    текст в кодировке UTF-8 с BOM, первая пустая строка обязательна
  * Дополнительный файл регулярных выражений замен: [RP_RSET.TXT](src/Pycckuu__Tatiana%202.30/RP_RSET.TXT),
    текст в кодировке UTF-8 с BOM, первая пустая строка обязательна, выполняется перед RULESET.TXT

***

* Готовый голос: [Pycckuu__Tatiana.vpm](dist/Pycckuu__Tatiana%202.30/Pycckuu__Tatiana.vpm)
* Исходные файлы голоса: [Pycckuu__Tatiana 2.30](src/Pycckuu__Tatiana%202.30/)
* Исходный словари:
  * Полный финальный словарь: [dictionary.dct](src/dictionary.dct),
    текстовый формат DCT в кодировке UTF-8 с BOM
  * Черновой словарь для русских названий: [dictionary.voc](src/dictionary.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Черновой словарь для названий городов: [cities.voc](src/cities.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Черновой словарь для иностранных названий: [dictionary_foreign.voc](src/dictionary_foreign.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Чистовой словарь для иностранных названий с восстановленными европейскими символами:
    [dictionary_foreign.dct](src/dictionary_foreign.dct),
    текстовый формат DCT в кодировке UTF-8 с BOM
* Компиляция словаря: [build_dictionary.bat](src/build_dictionary.bat),
  запускающий скопилированный [Python](https://www.python.org/download/releases/2.5.4/)-скрипт `dictcpl.pyc`
* Сборка голоса: [make_voice_Tatiana.bat](src/make_voice_Tatiana.bat)

## Словарь городов
Названия населённых пунктов вынесены в отдельный словарь и включаются в финальные словари Милены обеих версий и Татьяны.
Также они входят в состав дополнительного словаря для голоса Милена из сборки
[Nuance Vocalizer Expressive 2 TTS for Android от 4pda.to](https://4pda.to/forum/index.php?showtopic=200728)
для использования вместе с приложением [MapcamDroid](https://mapcam.info/forum/index.php?topic=1525.0),
которое может озвучивать оповещения о камерах и прочем, включая названия населённых пунктов, посредством TTS.

* Готовый словарь: [userdct_rur_milena.dat](dist/Vocalizer%20Expressive%203%20Milena/userdct_rur_milena.dat)
* Вспомогательные регулярные выражения коррекции произношения: [rex_rur.dat](dist/Vocalizer%20Expressive%203%20Milena/rex_rur.dat),
  текст в кодировке UTF-8 с BOM, первая пустая строка обязательна
* Исходный словари:
  * Полный финальный словарь: [alerts.dct](src/alerts.dct),
    текстовый формат DCT в кодировке UTF-8 с BOM
  * Черновой словарь для коррекции некоторых оповещений MapcamDroid: [alerts.voc](src/alerts.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
  * Черновой словарь для названий городов: [cities.voc](src/cities.voc),
    текстовый формат, похожий на DCT в кодировке UTF-8 с BOM для редактора
    [Milan](https://4pda.to/forum/index.php?s=&showtopic=200728&view=findpost&p=20937413)
* Компиляция словаря: [build_dictionary_alerts.bat](src/build_dictionary_alerts.bat),
  запускающий скопилированный [Python](https://www.python.org/download/releases/2.5.4/)-скрипт `dictcpl.pyc`

[Готовый словарь](https://github.com/yuryleb/garmin-russian-tts-voices/raw/master/dist/Vocalizer%20Expressive%203%20Milena/userdct_rur_milena.dat) и, по желанию,
[файл регулярных выражений](https://github.com/yuryleb/garmin-russian-tts-voices/raw/master/dist/Vocalizer%20Expressive%203%20Milena/rex_rur.dat)
копируются в папку данных голоса на устройстве Android с названием `VocalizerEx2` согласно
[инструкции по установке](https://4pda.to/forum/index.php?act=findpost&pid=95121276&anchor=Spoil-95121276-18).

## Ссылки
* [Последние версии обновленных голосов](https://github.com/yuryleb/garmin-russian-tts-voices/releases)
* ~~[Форум обсуждения недостатков Милены и других TTS-голосов у Garmin](http://nuvi.ru/forum/forum36/topic15794/)~~
* [Обсуждение фонетики названий, встроенной в карты HERE, используемых в Garmin](https://forum.mapcreator.here.com/forums/topic/%d1%84%d0%be%d0%bd%d0%b5%d1%82%d0%b8%d0%ba%d0%b0-%d1%80%d1%83%d1%81%d1%81%d0%ba%d0%b8%d1%85-%d0%bd%d0%b0%d0%b7%d0%b2%d0%b0%d0%bd%d0%b8%d0%b9-%d0%b2-%d0%ba%d0%b0%d1%80%d1%82%d0%b5-here/)
* [Форум использования TTS-голосов в Android на 4pda.to](https://4pda.to/forum/index.php?showtopic=200728)


## Примечания
* Словари, включающие падежные формы названий, и файлы регулярных выражений, одинаковы для
  Милены второго поколения и Татьяны
* Готовый словарь городов обновляется при каждом изменении исходных словарей и
  не включается в версии голосов для Garmin
* Для просмотра ссылок на форум https://4pda.to/forum/ нужна предварительная регистрация
