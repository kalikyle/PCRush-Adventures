INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the Storage Material Exchanger in this town... #speaker: StorageExchanger #portrait: 4
Do you want to Exhange your Materials for some Storages?? #speaker: StorageExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: storageexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: storageexchangeno #portrait: 0
    Ok, See you soon.. #speaker: StorageExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: StorageExchanger #portrait: 4
-> DONE