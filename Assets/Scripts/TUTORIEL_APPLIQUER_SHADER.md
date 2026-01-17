# 📚 TUTORIEL : Comment Appliquer un Shader à un Material

## 🎯 Objectif

Changer le shader de vos materials pour utiliser `Custom/URP Caustics Lit`

---

## 🔍 Méthode 1 : Via l'Objet dans la Scène (RECOMMANDÉ)

### ÉTAPE 1 : Sélectionner l'Objet

```
1. Allez dans la fenêtre "Hierarchy" (à gauche)
2. Cherchez "fond d'ocean_V1 (1)"
3. Cliquez dessus pour le sélectionner
```

**Visual** :
```
Hierarchy
├── Directional Light
├── Global Volume
├── HoverCar
├── sol
├── FishSchool
├── fond d'ocean_V1 (1)  ← CLIQUEZ ICI
└── Water Surface
```

### ÉTAPE 2 : Voir les Materials

```
1. Regardez la fenêtre "Inspector" (à droite)
2. Descendez jusqu'à voir "Mesh Renderer"
3. Cliquez sur la petite flèche à côté de "Materials" pour déplier
```

**Visual dans l'Inspector** :
```
Inspector
┌─────────────────────────────────┐
│ fond d'ocean_V1 (1)             │
├─────────────────────────────────┤
│ Transform                       │
│   Position  X Y Z               │
│   Rotation  X Y Z               │
│                                 │
│ Mesh Renderer                   │
│   ▼ Materials                   │ ← DÉPLIEZ ICI
│     Size: 3                     │
│     Element 0: Material1        │
│     Element 1: Material2        │
│     Element 2: Material3        │
└─────────────────────────────────┘
```

### ÉTAPE 3 : Ouvrir un Material

```
Pour CHAQUE material de la liste :

1. Cliquez sur le NOM du material (ex: "Material1")
   OU
2. Double-cliquez sur la petite icône ronde à gauche du nom
```

**Exemple** :
```
Materials
  Size: 3
  Element 0: [📦] Material1  ← CLIQUEZ sur "Material1"
  Element 1: [📦] Material2
  Element 2: [📦] Material3
```

### ÉTAPE 4 : Changer le Shader

Maintenant l'Inspector affiche les propriétés du material :

```
Inspector (Material)
┌─────────────────────────────────┐
│ Material1                       │
├─────────────────────────────────┤
│ Shader: Universal Render...     │ ← CLIQUEZ ICI
│                                 │
│ [Propriétés du shader...]       │
└─────────────────────────────────┘
```

**Actions** :
```
1. Cliquez sur "Shader: Universal Render..."
2. Un menu déroulant s'ouvre
3. Cherchez "Custom"
4. Sélectionnez "Custom/URP Caustics Lit"
```

**Menu qui apparaît** :
```
Shader Menu
├── Custom                    ← OUVREZ ICI
│   └── URP Caustics Lit      ← CLIQUEZ ICI
├── Shader Graphs
├── Universal Render Pipeline
│   ├── Lit
│   ├── Simple Lit
│   └── Unlit
└── ...
```

### ÉTAPE 5 : Répéter pour Tous les Materials

```
Répétez les ÉTAPES 3 et 4 pour :
  ✅ Element 0: Material1
  ✅ Element 1: Material2
  ✅ Element 2: Material3
  ✅ Tous les autres materials...
```

---

## 🔍 Méthode 2 : Via le Project (Alternative)

Si vous préférez modifier les materials directement :

### ÉTAPE 1 : Trouver les Materials

```
1. Fenêtre "Project" (en bas)
2. Cherchez dans vos dossiers de materials
   (probablement dans Assets/Materials ou Assets/3D_Assets)
3. Utilisez la barre de recherche : tapez "t:Material"
```

**Visual** :
```
Project
├── Assets
│   ├── Materials
│   │   ├── Material1        ← VOS MATERIALS
│   │   ├── Material2        ← SONT ICI
│   │   └── Material3
│   ├── 3D_Assets
│   │   └── fond d'ocean_V1
│   │       └── Materials    ← OU ICI
│   └── ...
```

### ÉTAPE 2 : Sélectionner un Material

```
1. Cliquez sur le material dans le Project
2. L'Inspector affiche ses propriétés
```

### ÉTAPE 3 : Changer le Shader

```
1. Dans l'Inspector, cliquez sur "Shader: ..."
2. Sélectionnez "Custom/URP Caustics Lit"
```

### ÉTAPE 4 : Répéter pour Tous

```
Faites ça pour chaque material utilisé par fond d'ocean_V1 (1)
```

---

## 📝 Méthode 3 : Changement en Masse (RAPIDE)

Si vous avez beaucoup de materials :

### OPTION A : Sélection Multiple

```
1. Fenêtre Project
2. Maintenez Ctrl (Windows) ou Cmd (Mac)
3. Cliquez sur tous les materials que vous voulez modifier
4. Dans l'Inspector, changez le Shader
5. Le shader change pour TOUS les materials sélectionnés !
```

**Visual** :
```
Project (sélection multiple)
├── Material1  ✓ (sélectionné)
├── Material2  ✓ (sélectionné)
├── Material3  ✓ (sélectionné)

Inspector
  Shader: Custom/URP Caustics Lit  ← Change tous !
```

### OPTION B : Créer un Nouveau Material et l'Assigner

```
1. Project → Clic droit → Create → Material
2. Nommez-le "OceanFloorCaustics"
3. Shader → Custom/URP Caustics Lit
4. Configurez les couleurs/textures
5. Hierarchy → fond d'ocean_V1 (1)
6. Inspector → Mesh Renderer → Materials
7. Glissez votre nouveau material sur tous les slots
```

---

## 🎨 Guide Visual Complet

### Vue d'Ensemble de l'Interface

```
┌─────────────────────────────────────────────────────────────┐
│ Unity Editor                                                │
├──────────────┬──────────────────────────────┬───────────────┤
│              │                              │               │
│  Hierarchy   │         Scene View           │   Inspector   │
│              │                              │               │
│  ▼ Items     │                              │  Properties   │
│    ├─ Light  │     [3D Scene View]          │  of Selected  │
│    ├─ Camera │                              │     Object    │
│    ├─ fond   │                              │               │
│    └─ ...    │                              │  Shader: ...  │
│              │                              │               │
├──────────────┴──────────────────────────────┤               │
│                                             │               │
│           Project (Assets)                  │               │
│                                             │               │
│  📁 Assets                                  │               │
│    📁 Materials                             │               │
│    📁 Scripts                               │               │
└─────────────────────────────────────────────┴───────────────┘
```

### Où Cliquer - Étape par Étape

**1. Sélection de l'Objet** :
```
Hierarchy (Gauche)
│
├─ fond d'ocean_V1 (1)  ← 1️⃣ CLIC ICI
│
```

**2. Dans l'Inspector** :
```
Inspector (Droite)
│
├─ Mesh Renderer
│  └─ Materials
│     ├─ Element 0: Material1  ← 2️⃣ CLIC sur "Material1"
│
```

**3. Changer le Shader** :
```
Inspector (Après clic sur Material1)
│
├─ Shader: Universal...  ← 3️⃣ CLIC ICI
│  └─ Menu déroulant s'ouvre
│     └─ Custom
│        └─ URP Caustics Lit  ← 4️⃣ CLIC ICI
│
```

---

## ✅ Comment Savoir si C'est Réussi ?

### Vérification Visuelle

Après avoir changé le shader, l'Inspector du material devrait afficher :

```
Inspector (Material)
┌─────────────────────────────────┐
│ Material1                       │
├─────────────────────────────────┤
│ Shader: Custom/URP Caustics Lit │ ← CORRECT ✅
│                                 │
│ Surface Options                 │
│   Base Map                      │
│   Base Color                    │
│   Smoothness                    │
│   Metallic                      │
│   Use Normal Map ☐              │
│   Normal Map                    │
│   Enable Caustics ☑             │ ← NOUVEAU
└─────────────────────────────────┘
```

**Signes que ça fonctionne** :
- ✅ Shader indique "Custom/URP Caustics Lit"
- ✅ Nouvelle option "Enable Caustics" apparaît
- ✅ Pas de messages d'erreur roses/magenta dans la scène

---

## 🐛 Problèmes Courants

### "Je ne trouve pas Custom/URP Caustics Lit"

**Vérification** :
```
1. Project → Assets/Scripts
2. Cherchez "URP_CausticsLit.shader"
3. Si absent → Le shader n'existe pas
4. Si présent → Attendez que Unity compile
```

**Solution** :
```
Si le shader est présent mais n'apparaît pas :
1. Sélectionnez le fichier .shader dans le Project
2. Clic droit → Reimport
3. Attendez la compilation
4. Réessayez
```

### "Le Material Devient Rose/Magenta"

**Cause** : Erreur de shader

**Solution** :
```
1. Console (en bas) → Regardez les erreurs
2. Si erreur de compilation :
   - Vérifiez que URP est installé
   - Vérifiez la version Unity (6000.0)
3. Contactez-moi avec les erreurs
```

### "Je Ne Vois Pas les Materials dans l'Inspector"

**Solution** :
```
1. Vérifiez que fond d'ocean_V1 (1) a un Mesh Renderer
2. Inspector → Cherchez "Mesh Renderer"
3. Si absent → L'objet n'a pas de materials
4. Si présent → Dépliez "Materials"
```

### "Mesh Renderer est Grisé"

**Cause** : L'objet est désactivé

**Solution** :
```
1. Hierarchy → Vérifiez que fond d'ocean_V1 (1) a une ✓
2. Si grisé → Clic droit → Activate
```

---

## 💡 Astuces Pratiques

### Raccourcis Clavier

```
Ctrl + P (Cmd + P sur Mac)  → Chercher shader par nom
Ctrl + Clic                 → Sélection multiple
F2                          → Renommer
Delete                      → (Ne pas utiliser sur materials !)
```

### Recherche Rapide de Shader

Dans le champ Shader :
```
1. Cliquez sur "Shader: ..."
2. Tapez directement "caustics"
3. Le menu filtre automatiquement
4. Sélectionnez "Custom/URP Caustics Lit"
```

### Copier les Paramètres

Si vous voulez que tous vos materials aient les mêmes réglages :

```
1. Configurez un material complètement
2. Inspector → Clic droit sur le nom du Shader
3. "Copy Component"
4. Sur un autre material → Clic droit
5. "Paste Component Values"
```

---

## 🎬 Exemple Complet : fond d'ocean_V1 (1)

### Scénario Complet

Supposons que `fond d'ocean_V1 (1)` a 3 materials :

**AVANT** :
```
fond d'ocean_V1 (1)
  Mesh Renderer
    Materials (3)
      Element 0: ocean_floor_mat     [Shader: URP/Lit]
      Element 1: ocean_rocks_mat     [Shader: URP/Lit]
      Element 2: ocean_sand_mat      [Shader: URP/Lit]
```

**ACTIONS** :

**Pour ocean_floor_mat** :
```
1. Clic sur "ocean_floor_mat"
2. Inspector → Shader: URP/Lit → CLIC
3. Menu → Custom → URP Caustics Lit → CLIC
4. ✅ Shader changé !
```

**Pour ocean_rocks_mat** :
```
1. Clic sur "ocean_rocks_mat"
2. Inspector → Shader: URP/Lit → CLIC
3. Menu → Custom → URP Caustics Lit → CLIC
4. ✅ Shader changé !
```

**Pour ocean_sand_mat** :
```
1. Clic sur "ocean_sand_mat"
2. Inspector → Shader: URP/Lit → CLIC
3. Menu → Custom → URP Caustics Lit → CLIC
4. ✅ Shader changé !
```

**APRÈS** :
```
fond d'ocean_V1 (1)
  Mesh Renderer
    Materials (3)
      Element 0: ocean_floor_mat     [Shader: Custom/URP Caustics Lit] ✅
      Element 1: ocean_rocks_mat     [Shader: Custom/URP Caustics Lit] ✅
      Element 2: ocean_sand_mat      [Shader: Custom/URP Caustics Lit] ✅
```

**RÉSULTAT** :
```
Play ▶️ → Les 3 materials ont maintenant des caustiques ! ✨
```

---

## 📋 Checklist Finale

Cochez au fur et à mesure :

```
[ ] 1. Hierarchy → Sélectionner fond d'ocean_V1 (1)
[ ] 2. Inspector → Voir Mesh Renderer → Materials
[ ] 3. Noter le nombre de materials (Size: ?)
[ ] 4. Pour le Material 1 :
    [ ] a. Cliquer sur le nom
    [ ] b. Changer Shader → Custom/URP Caustics Lit
    [ ] c. Vérifier qu'il n'est pas rose
[ ] 5. Pour le Material 2 :
    [ ] a. Cliquer sur le nom
    [ ] b. Changer Shader → Custom/URP Caustics Lit
    [ ] c. Vérifier qu'il n'est pas rose
[ ] 6. Pour le Material 3 (si existe) :
    [ ] a. Cliquer sur le nom
    [ ] b. Changer Shader → Custom/URP Caustics Lit
    [ ] c. Vérifier qu'il n'est pas rose
[ ] 7. Répéter pour tous les autres materials
[ ] 8. Play ▶️ → Tester
[ ] 9. Descendre sous l'eau → Voir les caustiques ✨
```

---

## 🎊 C'est Fait !

Une fois que tous vos materials utilisent `Custom/URP Caustics Lit` :

**Les caustiques apparaîtront automatiquement sous Y = 0 ! 🌊✨**

**Temps estimé** : 2-3 minutes pour 3-5 materials

---

## ❓ Besoin d'Aide ?

Si vous êtes bloqué :

1. **Vérifiez la Console** (en bas) pour les erreurs
2. **Prenez une capture d'écran** de votre Inspector
3. **Notez** le nombre de materials sur fond d'ocean_V1 (1)
4. **Demandez** et je vous aiderai !

**Vous pouvez le faire ! C'est simple ! 💪**
