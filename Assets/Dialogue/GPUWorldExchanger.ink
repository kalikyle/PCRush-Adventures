INCLUDE Global.ink

// Dialogue Start
Hi {player_Name}, I am the GPU Material Exchanger in this town... #speaker: GPUExchanger #portrait: 4
Do you want to Exhange your Materials for some GPUs?? #speaker: GPUExchanger #portrait: 4
* [Yes] #speaker: {player_Name} #choice: gpuexchangeyes #portrait: 0
    -> DONE
* No #speaker: {player_Name} #choice: gpuexchangeno #portrait: 0
    Ok, See you soon.. #speaker: GPUExchanger #portrait: 4
    -> DONE

=== Thank ===
Thank you come back again! #speaker: GPUExchanger #portrait: 4
-> DONE