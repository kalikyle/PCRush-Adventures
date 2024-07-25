INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the Case Material Exchanger in this town... #speaker: CaseExchanger #portrait: 4
Do you want to Exhange your Materials for some Cases?? #speaker: CaseExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: caseexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: caseexchangeno #portrait: 0
    Ok, See you soon.. #speaker: CaseExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: CaseExchanger #portrait: 4
-> DONE