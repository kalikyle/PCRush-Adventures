INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the finest helmet dealer in this town... #speaker: helmet Dealer #portrait: 3
Do you want to buy some? #speaker: helmet Dealer #portrait: 3
* [Yes] #speaker: {player_Name} #choice: helmetyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: helmetno #portrait: 0
    Ok, See you soon.. #speaker: helmet Dealer #portrait: 3
    -> DONE

=== Thank ===
Thank you come back again! #speaker: helmet Dealer #portrait: 3
-> DONE