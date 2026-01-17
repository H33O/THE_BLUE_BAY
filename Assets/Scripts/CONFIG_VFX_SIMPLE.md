# ⚡ CONFIG VFX - Version Ultra-Simple

## 🎯 Vous avez UnderwaterCaustic.vfx OUVERT devant vous

Voici EXACTEMENT quoi faire, étape par étape avec images mentales :

---

## PARTIE 1 : BLACKBOARD (Panneau de GAUCHE)

### Vous voyez quoi ?

```
┌─────────────────────┐
│ Blackboard          │
├─────────────────────┤
│                     │
│  (vide ou presque)  │
│                     │
│         [+]         │ ← Ce bouton !
└─────────────────────┘
```

### Cliquez sur le [+] et ajoutez 6 propriétés :

**1. Clic [+] → Float**
```
Nom: Intensity
Cochez ☑️ Exposed
Value: 1.0
```

**2. Clic [+] → Float**
```
Nom: AnimationSpeed
Cochez ☑️ Exposed
Value: 0.5
```

**3. Clic [+] → Float**
```
Nom: Scale
Cochez ☑️ Exposed
Value: 1.5
```

**4. Clic [+] → Vector4**
```
Nom: CausticsColor
Cochez ☑️ Exposed
Value: X=0.4, Y=0.8, Z=1.0, W=1.0
```

**5. Clic [+] → Float**
```
Nom: WaterLevel
Cochez ☑️ Exposed
Value: 0
```

**6. Clic [+] → Float**
```
Nom: EffectDepth
Cochez ☑️ Exposed
Value: 50
```

### Résultat Final du Blackboard :

```
┌─────────────────────────┐
│ Blackboard              │
├─────────────────────────┤
│ ☑️ Intensity (Float)    │
│ ☑️ AnimationSpeed       │
│ ☑️ Scale                │
│ ☑️ CausticsColor (Vec4) │
│ ☑️ WaterLevel           │
│ ☑️ EffectDepth          │
└─────────────────────────┘
```

✅ C'est fait ? Passez à la Partie 2 !

---

## PARTIE 2 : LE GRAPH (Zone centrale)

### Vous voyez quoi ?

```
┌──────────────────────────────────────┐
│                                      │
│   [Spawn] → [Initialize] → [Update] → [Output]
│                                      │
└──────────────────────────────────────┘
```

### CONFIG MINIMAL (Le Plus Simple)

#### Sur SPAWN :

```
Clic sur [Spawn]
Rate: 1000
```

#### Sur INITIALIZE PARTICLE :

```
Clic sur [Initialize Particle]

Dans l'Inspector à droite, ajoutez des "blocks" :
  (Clic droit sur Initialize Particle → Add Block)

1. Add Block → "Set Capacity"
   → Capacity: 3000

2. Add Block → "Set Lifetime Random"
   → Min: 2
   → Max: 5

3. Add Block → "Set Velocity Random"
   → Min: (-0.2, -1, -0.2)
   → Max: (0.2, -0.5, 0.2)

4. Add Block → "Set Position (Shape: Sphere)"
   → Center: (0, 0, 0)
   → Radius: 25

5. Add Block → "Set Size"
   → Size: 0.5

6. Add Block → "Set Color"
   → Glissez "CausticsColor" depuis Blackboard
```

#### Sur UPDATE PARTICLE :

```
Clic sur [Update Particle]

1. Add Block → "Turbulence"
   → Intensity: 0.3
```

#### Sur OUTPUT PARTICLE QUAD :

```
Clic sur [Output Particle Quad]

Changez :
  - Blend Mode: Additive
  - Orient: Face Camera Position
```

---

## ✅ SAUVEGARDE

**Ctrl + S** pour sauvegarder !

Le VFX est prêt ! 🎉

---

## 🎨 À QUOI ÇA RESSEMBLE (Schéma Final)

```
BLACKBOARD          GRAPH
┌─────────┐         ┌────────────────────────────────┐
│ Props   │         │                                │
│ ☑️ Intens│         │  [Spawn]                       │
│ ☑️ Speed │         │  Rate: 1000                    │
│ ☑️ Scale │         │     ↓                          │
│ ☑️ Color │    ←────┤  [Initialize]                  │
│ ☑️ Water │         │  - Capacity: 3000              │
│ ☑️ Depth │         │  - Lifetime: 2-5               │
└─────────┘         │  - Velocity Random             │
                    │  - Position Sphere             │
                    │  - Size: 0.5                   │
                    │  - Color: CausticsColor        │
                    │     ↓                          │
                    │  [Update]                      │
                    │  - Turbulence                  │
                    │     ↓                          │
                    │  [Output Quad]                 │
                    │  - Blend: Additive             │
                    │  - Orient: Face Camera         │
                    └────────────────────────────────┘
```

---

## 🚫 PAS BESOIN DE COMPRENDRE TOUT !

**L'essentiel** :
- Les propriétés permettent de contrôler depuis le script
- Le graph crée des particules qui bougent
- L'output les affiche en mode additif (lumière)

**Vous pouvez tweaker plus tard !**

Pour l'instant, **juste suivez les étapes** ✅

Ctrl + S et on passe à l'étape suivante !
