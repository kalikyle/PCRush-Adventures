INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the RAM Material Exchanger in this town... #speaker: RAMExchanger #portrait: 4
Do you want to Exhange your Materials for some RAMs?? #speaker: RAMExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: ramexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: ramexchangeno #portrait: 0
    Ok, See you soon.. #speaker: RAMExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: RAMExchanger #portrait: 4
-> DONE