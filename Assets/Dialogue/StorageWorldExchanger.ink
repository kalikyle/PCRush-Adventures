INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the Storage Material Exchanger in this town... #speaker: Storage Exchanger #portrait: 14
Do you want to Exhange your Materials for some Storages?? #speaker: Storage Exchanger #portrait: 14
* [Yes] #speaker: {player_Name} #choice: storageexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: storageexchangeno #portrait: 0
    Ok, See you soon.. #speaker: Storage Exchanger #portrait: 14
    -> DONE

=== Thank ===
Thank you come back again! #speaker: Storage Exchanger #portrait: 14
-> DONE