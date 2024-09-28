INCLUDE Global.ink
#range: 15,17

// Dialogue Start
Hi {player_Name}, I am the finest sword dealer in this town... #speaker: Sword Dealer #portrait: 9
Do you want to buy some? #speaker: Sword Dealer #portrait: 9
* [Yes] #speaker: {player_Name} #choice: swordyes #portrait: 0 
    -> DONE
* No #speaker: {player_Name} #choice: swordno #portrait: 0 
    Ok, See you soon.. #speaker: Sword Dealer #portrait: 9
    -> DONE

=== Thank ===
Thank you come back again! #speaker: Sword Dealer #portrait: 9
-> DONE