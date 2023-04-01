```
    _____       _     _   _ _   _      _____  _           _                       
    / ____|     | |   | | (_) | | |    |  __ \(_)         | |                      
   | (___  _   _| |__ | |_ _| |_| | ___| |  | |_ ___ _ __ | | __ _ _   _  ___ _ __ 
    \___ \| | | | '_ \| __| | __| |/ _ \ |  | | / __| '_ \| |/ _` | | | |/ _ \ '__|
    ____) | |_| | |_) | |_| | |_| |  __/ |__| | \__ \ |_) | | (_| | |_| |  __/ |   
   |_____/ \__,_|_.__/ \__|_|\__|_|\___|_____/|_|___/ .__/|_|\__,_|\__, |\___|_|   
                                                    | |             __/ |          
```

⭐ Star us on GitHub — it helps!

[![repo-size](https://img.shields.io/github/languages/code-size/imacwink/SubtitleDisplayer?style=flat)](https://github.com/imacwink/SubtitleDisplayer/archive/main.zip) [![tag](https://img.shields.io/github/v/tag/imacwink/SubtitleDisplayer)](https://github.com/imacwink/SubtitleDisplayer/tags) [![license](https://img.shields.io/github/license/imacwink/SubtitleDisplayer)](LICENSE) 

## Introduction
> A small case in recent work requires the parsing and display of SRT format subtitles through the Unity engine, so I wrote this example for recording.

## Environment
> Unity 2019.4.19f1.

## SRT File

> Every SRT file is made up of four components:

```
1
00:00:03,366 --> 00:00:05,766
高老夫人：翠兰翠兰

2
00:00:06,300 --> 00:00:08,566
高老夫人：孩子翠兰
```

- Number of the caption frame in sequence
- Beginning & ending timecodes for when caption frame should appear
- The caption itself
- Blank line indicating the start of new caption sequence


