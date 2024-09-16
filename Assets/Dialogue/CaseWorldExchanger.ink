INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the Case Material Exchanger in this town... #speaker: Case Exchanger #portrait: 17
Do you want to Exhange your Materials for some Cases?? #speaker: Case Exchanger #portrait: 17
* [Yes] #speaker: {player_Name} #choice: caseexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: caseexchangeno #portrait: 0
    Ok, See you soon.. #speaker: Case Exchanger #portrait: 17
    -> DONE

=== ThankCase ===
Thank you come back again! #speaker: Case Exchanger #portrait: 17
-> DONE