INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}... #speaker: Computer Parts Buyer #portrait: 3
Hello #speaker: {player_Name}  #portrait: 0
I Buy some computer parts, do you have any computer parts?, I Buy will it #speaker: Computer Parts Buyer #portrait: 3
* [Yes, I want to sell some] #speaker: {player_Name} #portrait: 0 #choice: sellyes
    -> DONE
* No, I don't want to sell #speaker: {player_Name} #choice: sellno #portrait: 0 
    Ok, See you soon.. #speaker: Computer Parts Buyer #portrait: 3
    -> DONE

=== Thank ===
Thank you, If you have any computer parts to sell, just visit me again #speaker: Computer Parts Buyer #portrait: 3
-> DONE