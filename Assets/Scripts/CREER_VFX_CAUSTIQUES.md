# 🎨 CRÉER : VFX Graph Caustiques Sous-Marines

## 🎯 Vue d'Ensemble

Vous allez créer un effet VFX de caustiques projetées qui :
- ✅ Fonctionne sans modifier vos materials
- ✅ S'active automatiquement sous l'eau
- ✅ Couvre uniquement la zone sous Y = 0
- ✅ Effet spectaculaire et performant

---

## 📦 PRÉREQUIS

**Avant de commencer, installez le package VFX Graph** :

```
Window → Package Manager
Recherchez "Visual Effect Graph"
Install
```

Voir `INSTALLER_VFX_GRAPH.md` pour les détails.

---

## 🚀 CRÉATION DU VFX GRAPH

### ÉTAPE 1 : Créer le VFX Asset

```
1. Project → Assets
2. Clic droit → Create → Visual Effects → Visual Effect Graph
3. Nommez-le "UnderwaterCaustics"
```

**Résultat** :
```
Assets
└── UnderwaterCaustics.vfx  ← Créé
```

### ÉTAPE 2 : Ouvrir le VFX Graph Editor

```
1. Double-cliquez sur "UnderwaterCaustics.vfx"
2. L'éditeur VFX Graph s'ouvre
```

**Visual** :
```
┌─────────────────────────────────────────────┐
│ VFX Graph                                   │
├─────────────────────────────────────────────┤
│                                             │
│  [Initialize] → [Update] → [Output]        │
│                                             │
└─────────────────────────────────────────────┘
```

---

## 🎨 CONFIGURATION DU GRAPH

### ÉTAPE 3 : Créer les Propriétés Exposées

Dans le **Blackboard** (à gauche de l'éditeur VFX Graph) :

**Cliquez sur "+" pour ajouter des propriétés** :

```
Propriétés à créer :

1. Intensity (Float)
   - Default: 1.0
   - Range: 0 to 2

2. AnimationSpeed (Float)
   - Default: 0.5
   - Range: 0 to 2

3. Scale (Float)
   - Default: 1.5
   - Range: 0.5 to 5

4. CausticsColor (Vector4)
   - Default: (0.4, 0.8, 1.0, 1.0)

5. WaterLevel (Float)
   - Default: 0

6. EffectDepth (Float)
   - Default: 50
```

**Comment créer une propriété** :
```
1. Blackboard → "+" → Float (ou Vector4)
2. Nommez la propriété (ex: "Intensity")
3. Cochez "Exposed" pour la rendre accessible au script
4. Définissez la valeur par défaut
```

---

## 🔧 CONFIGURATION SIMPLE (Version Rapide)

Si vous voulez une version simple pour tester :

### Version Minimaliste

**Initialize Particle** :
```
- Capacity: 5000
- Set Lifetime Random: 2 to 4
- Set Velocity Random: (-0.1, -1, -0.1) to (0.1, -0.5, 0.1)
- Set Position: Random (Sphere) Radius: 25
- Set Size: 0.5
- Set Color: CausticsColor
```

**Update Particle** :
```
- Turbulence: Intensity 0.2
- Conform to Sphere: Center (0, WaterLevel, 0), Radius: EffectDepth
```

**Output Particle Quad** :
```
- Blend Mode: Additive
- Color Mapping: Random → CausticsColor
- Size: 0.5 * Scale
```

---

## 🎯 CONFIGURATION AVANCÉE (Version Spectaculaire)

### System Setup

**Spawn** :
```
- Rate: 1000
- Burst Count: 500
```

### Initialize Context

**Ajouter les blocks suivants** (clic droit → Create Block) :

```
1. Set Lifetime Random
   - Min: 2
   - Max: 5

2. Set Velocity Random
   - Min: (-0.2, -1.5, -0.2)
   - Max: (0.2, -0.5, 0.2)

3. Set Position (Shape: Cylinder)
   - Center: (0, WaterLevel + 2, 0)
   - Height: 5
   - Radius: 30

4. Set Size Random
   - Min: 0.3 * Scale
   - Max: 0.8 * Scale

5. Set Color
   - Mode: Random
   - Color A: CausticsColor
   - Color B: CausticsColor * 0.5
```

### Update Context

```
1. Age Particles
   (déjà présent par défaut)

2. Turbulence
   - Intensity: 0.3
   - Frequency: 0.5
   - Octaves: 3

3. Vector Field Force
   - Direction: (0, -1, 0)
   - Intensity: 0.5

4. Conform to Sphere
   - Center: (0, WaterLevel - EffectDepth/2, 0)
   - Radius: EffectDepth
   - Attraction Speed: 2
   - Attraction Force: 5
   - Stick Distance: 0.1

5. Set Size over Life
   - Curve: 0 → 1 → 0 (fade in/out)
   - Multiply: Size

6. Set Color over Life
   - Gradient: Transparent → Full → Transparent
```

### Output Particle Quad

```
1. Blend Mode: Additive

2. Orient: Face Camera Position

3. Color Mapping: Color

4. Size: Size

5. Sort: Distance to Camera
```

---

## 🎨 CONFIGURATION VISUELLE

### Ajouter un Material Personnalisé (Optionnel)

Si vous voulez un meilleur rendu :

```
1. Project → Create → Material
2. Nommez "CausticParticle"
3. Shader → Visual Effects → Default Particle Unlit
4. Base Map → (Texture de caustiques si vous en avez)
5. Blend Mode → Additive

Dans VFX Output :
  - Assign Material: CausticParticle
```

---

## 🌊 PLACEMENT DANS LA SCÈNE

### ÉTAPE 4 : Créer le GameObject VFX

```
1. Hierarchy → Clic droit → Visual Effects → Visual Effect
2. Nommez-le "Underwater Caustics VFX"
3. Position: (0, waterLevel, 0)  [ex: 0, 0, 0]
```

### ÉTAPE 5 : Assigner le VFX Graph

```
1. Sélectionnez "Underwater Caustics VFX" dans Hierarchy
2. Inspector → Visual Effect component
3. Asset Template → Glissez "UnderwaterCaustics.vfx"
```

### ÉTAPE 6 : Ajouter le Contrôleur

```
1. Avec "Underwater Caustics VFX" sélectionné
2. Inspector → Add Component
3. Cherchez "VFXCausticsController"
4. Ajoutez-le
```

**Configuration du VFXCausticsController** :
```
Inspector (Underwater Caustics VFX)
├── Visual Effect
│   └── Asset Template: UnderwaterCaustics.vfx
│
└── VFX Caustics Controller (Script)
    ├── Caustics VFX: (auto-référencé)
    ├── Water Level: 0
    ├── Effect Depth: 50
    ├── Intensity: 1
    ├── Animation Speed: 0.5
    ├── Scale: 1.5
    ├── Caustics Color: Cyan
    └── Enable Caustics: ✓
```

---

## 🔗 INTÉGRATION AVEC LE SYSTÈME UNDERWATER

### ÉTAPE 7 : Connecter au UnderwaterEffectController

```
1. Hierarchy → HoverCar/Main Camera
2. Inspector → UnderwaterEffectController
3. Section "Caustiques VFX"
4. VFX Caustics Controller → Glissez "Underwater Caustics VFX"
```

**Résultat** :
```
HoverCar/Main Camera
└── UnderwaterEffectController
    ├── Water Level: 0
    ├── Underwater Volume: [...]
    └── VFX Caustics Controller: Underwater Caustics VFX  ← LIEN
```

---

## ✅ TEST

### ÉTAPE 8 : Tester l'Effet

```
1. Sauvegardez la scène (Ctrl + S)
2. Play ▶️
3. Descendez sous l'eau avec HoverCar
4. Les caustiques VFX s'activent ! ✨
```

**Comportement attendu** :

```
Au-dessus de l'eau (Y > 0) :
  → Intensity = 0
  → Pas de particules visibles

Sous l'eau (Y < 0) :
  → Intensity = 1
  → Particules de caustiques animées
  → Effet progressif (2 secondes de transition)
```

---

## 🎛️ AJUSTEMENTS

### Contrôler l'Intensité

```
Underwater Caustics VFX → VFXCausticsController
  - Intensity: 0.5 (discret) à 2.0 (spectaculaire)
```

### Changer la Zone Couverte

```
Underwater Caustics VFX → VFXCausticsController
  - Effect Depth: 20 (petit) à 100 (énorme)
```

### Modifier la Couleur

```
Underwater Caustics VFX → VFXCausticsController
  - Caustics Color: Cyan (défaut)
  - Ou blanc, bleu turquoise, etc.
```

### Vitesse d'Animation

```
Underwater Caustics VFX → VFXCausticsController
  - Animation Speed: 0.1 (lent) à 1.5 (rapide)
```

---

## 🎨 ASTUCES VISUELLES

### Pour un Effet Plus Réaliste

**Dans le VFX Graph** :

1. **Ajouter de la variation** :
   ```
   Initialize → Set Size Random
   Min: 0.2, Max: 1.5
   ```

2. **Mouvement ondulant** :
   ```
   Update → Noise
   Type: Perlin
   Frequency: 0.2
   Amplitude: 0.5
   ```

3. **Fade progressif** :
   ```
   Output → Color over Life
   Gradient: 0% → 100% → 0%
   ```

### Pour un Effet Spectaculaire

```
VFX Graph:
  - Spawn Rate: 2000 (plus de particules)
  - Particle Size: Plus grand (1.0 - 2.0)
  - Blend Mode: Additive (lumière intense)

Controller:
  - Intensity: 1.5 - 2.0
  - Caustics Color: Bleu brillant (0.3, 0.7, 1, 1)
```

---

## 🐛 DÉPANNAGE

### "Je ne vois aucune particule"

**Vérifications** :

1. **VFX est actif ?**
   ```
   Hierarchy → Underwater Caustics VFX
   Coché (activé) dans l'Inspector
   ```

2. **Asset assigné ?**
   ```
   Visual Effect → Asset Template: UnderwaterCaustics.vfx
   ```

3. **Intensity > 0 ?**
   ```
   VFXCausticsController → Intensity: 1.0
   Enable Caustics: ✓
   ```

4. **Position correcte ?**
   ```
   Transform → Position Y proche de waterLevel
   ```

5. **Play mode actif ?**
   ```
   Les VFX ne s'affichent qu'en Play mode
   ```

### "Les particules sont roses"

**Cause** : Shader manquant

**Solution** :
```
1. Vérifiez que VFX Graph package est bien installé
2. VFX Graph Editor → Output → Shader Graph: Default
```

### "L'effet ne s'active pas sous l'eau"

**Vérifications** :

1. **Lien correct ?**
   ```
   Main Camera → UnderwaterEffectController
   VFX Caustics Controller: [Référence assignée]
   ```

2. **Water Level correct ?**
   ```
   VFXCausticsController → Water Level: 0
   UnderwaterEffectController → Water Level: 0
   Doivent correspondre !
   ```

3. **Console ?**
   ```
   Regardez les messages :
   "Entré dans l'eau - Caustiques activées"
   ```

### "Performance basse"

**Optimisation** :

```
VFX Graph:
  - Spawn Rate: 500 (au lieu de 1000)
  - Capacity: 2000 (au lieu de 5000)
  - Output → Sorting: None

Contrôleur:
  - Effect Depth: 30 (plus petit)
```

---

## 📊 COMPARAISON AVEC SHADER

| Aspect | VFX Graph | Shader |
|--------|-----------|--------|
| **Setup** | Moyen (VFX Graph) | Simple (code) |
| **Materials** | ✅ Pas de modification | ❌ Doit changer shader |
| **Visuel** | ✅✅ Spectaculaire | ✅ Intégré |
| **Performance** | ⚠️ Moyenne | ✅✅ Excellente |
| **Flexibilité** | ✅✅ Très flexible | ⚠️ Fixe |
| **Contrôle Y** | ✅ Parfait | ✅ Parfait |

**Votre cas (materials FBX verrouillés)** :
→ **VFX Graph est la meilleure solution !** ✅

---

## 🎊 RÉSUMÉ

### Ce que Vous Avez Créé

```
Système VFX Caustiques Complet
│
├── Assets
│   └── UnderwaterCaustics.vfx      (Graph VFX)
│
├── Scripts
│   └── VFXCausticsController.cs     (Contrôle)
│
└── Scène
    ├── Underwater Caustics VFX      (GameObject)
    │   ├── Visual Effect
    │   └── VFXCausticsController
    │
    └── HoverCar/Main Camera
        └── UnderwaterEffectController (avec lien VFX)
```

### Avantages de Cette Solution

✅ **Zéro modification de materials**
✅ Fonctionne sur n'importe quel objet
✅ Contrôle précis par niveau Y
✅ Activation automatique sous l'eau
✅ Effet visuel spectaculaire
✅ Facile à tweaker

**Votre problème de materials FBX est résolu ! 🎉**

---

## 🚀 PROCHAINES ÉTAPES

1. ✅ Installez VFX Graph package
2. ✅ Créez le VFX Graph
3. ✅ Configurez les propriétés
4. ✅ Placez dans la scène
5. ✅ Testez !

**Temps estimé total : 15-20 minutes**

**Le résultat en vaut la peine ! 🌊✨**
