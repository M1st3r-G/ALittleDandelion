# A Little Dandelion
Prototyping Project
A Cozy Gardening Game with calming colors

---
ToDo:
- Remove Old/Dead Plants
- Pause menu
  - Add Volume Sliders

- BlumenBuch:
  - Name und Bleistift immer,
  - Color bei erster züchtung.
  - Hinweise Werden nach und nach freigeschaltet:
    - Jeder stern ein Hinweis.
    - Max 5.

- Saving the Game
  - SeedBox States
  - FlowerDisplay State
  - (FlowerBook State)
  - (ZuchtRegalState)
- Loading the Game
  - Set ...


Zuchtloop:
- Aufbewahren von Fertigen Pflanzen
- Auswahl und Kreuzung
- Samenernte (Nach einigen Tagen)

Enhancements:
- Fix Highlight
- Make Pot and SeedBox inherit from PlantContainer?
- Don't Unsubscribe from DisplayRefreshes (Later, because Debug can only render One)
  - PlatRender subscribes to the RefreshEvent?
  - Leads To Overlap in the DebugDisplay



Flower:

TOJSON:
- type -> flowerType
- private GrowthState _state 
  private int _lastWater   
  private int _growthCounter
  private int _rating
  private bool _isReplant