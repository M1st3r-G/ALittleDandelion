# A Little Dandelion
Prototyping Project
A Cozy Gardening Game with calming colors

---
ToDo:
- Add Label
- Add Models and Material
- Pause menu
  - Add Volume Sliders

- Behaltene Blumen loswerden, mit einem Verkaufen button
- BlumenBuch:
  - Name und Bleistift immer,
  - Color bei erster züchtung.
  - Hinweise Werden nach und nach freigeschaltet:
    - Jeder stern ein Hinweis.
    - Max 5.

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
- SaveFiles include Evniroment in seeds
- Let the SaveFile Controller Adjust the Data