INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the CPU Fan Material Exchanger in this town... #speaker: CPUFExchanger #portrait: 4
Do you want to Exhange your Materials for some CPU Fans?? #speaker: CPUFExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: cpufexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: cpufexchangeno #portrait: 0
    Ok, See you soon.. #speaker: CPUFExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: CPUFExchanger #portrait: 4
-> DONE