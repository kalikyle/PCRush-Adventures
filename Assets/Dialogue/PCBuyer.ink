INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}... #speaker: Computer Parts Buyer #portrait: 19
Hello #speaker: {player_Name}  #portrait: 0
I sell PCs to other people, Do you have any computer to sell? #speaker: Computer Parts Buyer #portrait: 19
* [Yes, I want to sell some] #speaker: {player_Name} #portrait: 0 #choice: sellPCsyes
    -> DONE
* No, thanks #speaker: {player_Name} #choice: sellPCsno #portrait: 0 
    Ok, See you soon.. #speaker: Computer Parts Buyer #portrait: 19
    -> DONE

=== Thank ===
Thank you, If you have any computers to sell, just visit me again #speaker: Computer Parts Buyer #portrait: 19
-> DONE