INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the PSU Material Exchanger in this town... #speaker: PSU Exchanger #portrait: 15
Do you want to Exhange your Materials for some PSUs?? #speaker: PSU Exchanger #portrait: 15
* [Yes] #speaker: {player_Name} #choice: psuexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: psuexchangeno #portrait: 0
    Ok, See you soon.. #speaker: PSU Exchanger #portrait: 15
    -> DONE

=== Thank ===
Thank you come back again! #speaker: PSU Exchanger #portrait: 15
-> DONE