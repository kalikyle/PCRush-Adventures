﻿INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the CPU Material Exchanger in this town... #speaker: CPUExchanger #portrait: 4
Do you want to Exhange your Materials for some CPUs?? #speaker: CPUExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: exchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: exchangeno #portrait: 0
    Ok, See you soon.. #speaker: CPUExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: CPUExchanger #portrait: 4
-> DONE