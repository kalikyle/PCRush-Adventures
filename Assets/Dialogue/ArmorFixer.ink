INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, Do you need to Fix Your Armor?... #speaker: ArmorFixer #portrait: 4
I can do that for you?? #speaker: ArmorFixer #portrait: 4
* [Yes] #speaker: {player_Name} #choice: fixarmor #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: nofixarmor #portrait: 0
    Ok, See you soon.. #speaker: ArmorFixer #portrait: 4
    -> DONE

=== Thank ===
Thats it, Come back again! #speaker: ArmorFixer #portrait: 4
-> DONE