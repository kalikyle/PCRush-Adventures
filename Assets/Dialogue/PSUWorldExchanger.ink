INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the PSU Material Exchanger in this town... #speaker: PSUExchanger #portrait: 4
Do you want to Exhange your Materials for some PSUs?? #speaker: PSUExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: psuexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: psuexchangeno #portrait: 0
    Ok, See you soon.. #speaker: PSUExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: PSUExchanger #portrait: 4
-> DONE