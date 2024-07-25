INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the Motherboard Material Exchanger in this town... #speaker: MotherboardExchanger #portrait: 4
Do you want to Exhange your Materials for some Motherboards?? #speaker: MotherboardExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: mbexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: mbexchangeno #portrait: 0
    Ok, See you soon.. #speaker: MotherboardExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: MotherboardExchanger #portrait: 4
-> DONE