INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the RAM Material Exchanger in this town... #speaker: RAM Exchanger #portrait: 11
Do you want to Exhange your Materials for some RAMs?? #speaker: RAM Exchanger #portrait: 11
* [Yes] #speaker: {player_Name} #choice: ramexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: ramexchangeno #portrait: 0
    Ok, See you soon.. #speaker: RAM Exchanger #portrait: 11
    -> DONE

=== Thank ===
Thank you come back again! #speaker: RAM Exchanger #portrait: 11
-> DONE