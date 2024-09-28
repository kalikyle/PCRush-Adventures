INCLUDE Global.ink
#range: 0,1

// Dialogue Start
Hi {player_Name}, I am the finest shield dealer in this town... #speaker: shield Dealer #portrait: 8
Do you want to buy some? #speaker: shield Dealer #portrait: 8
* [Yes] #speaker: {player_Name} #choice: shieldyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: shieldno #portrait: 0
    Ok, See you soon.. #speaker: shield Dealer #portrait: 8
    -> DONE

=== Thank ===
Thank you come back again! #speaker: shield Dealer #portrait: 8
-> DONE