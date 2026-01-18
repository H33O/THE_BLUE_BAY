# 🌿 GUIDE : Peindre des Plantes Sous-Marines

## 🎯 3 Solutions Disponibles

### ⭐ SOLUTION 1 : Foliage Painter (Script Custom - SIMPLE)

**Avantages** :
- ✅ Créé pour vous, prêt à l'emploi
- ✅ Fonctionne sur n'importe quelle surface
- ✅ Contrôle précis
- ✅ Mode effacement inclus

**Je recommande celle-ci pour commencer !**

---

## ⚡ UTILISATION : Foliage Painter

### ÉTAPE 1 : Créer le Tag "Foliage"

```
Menu Unity (en haut) → Edit → Project Settings
Tags and Layers → Tags
Cliquez sur "+"
Ajoutez le tag: "Foliage"
```

### ÉTAPE 2 : Créer des Prefabs de vos Plantes

Vos plantes sont probablement dans le pack Pandazole.

**Trouvez vos modèles** :
```
Project → Assets/Pandazole_Ultimate_Pack/.../Models
```

**Créez un dossier Prefabs** :
```
Project → Assets → Clic droit → Create → Folder
Nom: "Underwater Prefabs"
```

**Convertissez les modèles en prefabs** :
```
Option A : Glisser-déposer
  1. Glissez le modèle (ex: Coral_16) de Project vers la scène
  2. Ajustez si besoin (scale, rotation)
  3. Glissez de Hierarchy → Project/Underwater Prefabs
  4. Prefab créé ! Supprimez l'instance de la scène

Option B : Créer directement
  1. Project → Underwater Prefabs
  2. Clic droit → Create → Prefab
  3. Double-clic → Mode prefab
  4. Glissez le modèle dedans
```

**Assignez le tag "Foliage"** :
```
Sélectionnez le prefab
Inspector → Tag → Foliage
```

### ÉTAPE 3 : Créer le GameObject Painter

```
Hierarchy → Clic droit → Create Empty
Nom: "Foliage Painter"

Inspector → Add Component
Cherchez: "FoliagePainter"
Ajoutez-le
```

### ÉTAPE 4 : Configurer le Painter

```
Inspector (Foliage Painter)
┌─────────────────────────────────┐
│ Foliage Painter (Script)        │
├─────────────────────────────────┤
│ Foliage Prefabs                 │
│   Size: 5  ← Nombre de prefabs  │
│   Element 0: [Glissez prefab]   │
│   Element 1: [Glissez prefab]   │
│   Element 2: [Glissez prefab]   │
│   ...                           │
│                                 │
│ Brush Size: 10                  │
│ Density: 10                     │
│ Random Rotation: ✓              │
│ Scale Variation: 0.2            │
│                                 │
│ Parent Folder: [Auto]           │
│                                 │
│ Erase Mode: ☐                   │
│ Erase Tag: Foliage              │
│                                 │
│ [Créer Dossier Parent]          │
└─────────────────────────────────┘
```

**Glissez vos prefabs** dans la liste "Foliage Prefabs" !

### ÉTAPE 5 : PEINDRE ! 🎨

**Mode Peinture** :
```
1. Sélectionnez "Foliage Painter" dans Hierarchy
2. Vue Scene active (pas Game)
3. CTRL + Clic gauche sur la surface → Plantes apparaissent ! ✨
4. CTRL + Glisser pour peindre en continu
```

**Mode Effacement** :
```
CTRL + SHIFT + Clic → Efface les plantes dans la zone
```

**Cercle vert** = Zone de peinture (visible quand Foliage Painter est sélectionné)

### ÉTAPE 6 : Ajuster les Paramètres

**Brush Size** : Taille du cercle de peinture
- Petit (5) → Précis
- Grand (30) → Zone large

**Density** : Nombre de plantes par clic
- Faible (5) → Épars
- Élevé (30) → Dense

**Random Rotation** : Rotation aléatoire pour varier
- ✓ Recommandé

**Scale Variation** : Variation de taille
- 0 = Toutes identiques
- 0.3 = Variation ±30%

---

## 🎨 WORKFLOW RECOMMANDÉ

### 1. Préparer les Prefabs

```
Créez des prefabs pour chaque type de plante :
  ├─ Coraux (5-10 variantes)
  ├─ Algues (3-5 variantes)
  ├─ Rochers (2-3 variantes)
  └─ Décorations diverses
```

### 2. Organiser par Type

Créez **plusieurs Foliage Painters** :

```
Hierarchy
├─ Foliage Painter - Coraux
│   └─ Prefabs: Coral_01, Coral_02, Coral_03...
│
├─ Foliage Painter - Algues
│   └─ Prefabs: Seaweed_01, Seaweed_02...
│
└─ Foliage Painter - Rochers
    └─ Prefabs: Rock_01, Rock_02...
```

**Avantage** : Peindre par catégorie, contrôle précis !

### 3. Peindre par Couches

```
1. Fond de coraux (grand brush, basse densité)
2. Algues entre les coraux (brush moyen)
3. Petits détails (petit brush, haute densité)
4. Rochers et accents
```

### 4. Varier les Densités

```
Zone proche du joueur :
  - Density: 15-20
  - Brush Size: 5-10

Zone lointaine :
  - Density: 5-10
  - Brush Size: 20-30
```

---

## ⭐ SOLUTION 2 : Polybrush (Package Unity)

**Si vous préférez l'outil officiel Unity** :

### Installation

```
Window → Package Manager
"+" → Add package by name
com.unity.polybrush
Install
```

### Utilisation

```
Tools → Polybrush → Polybrush Window

Onglet "Scatter" :
  - Ajoutez vos prefabs
  - Sélectionnez la surface
  - Cliquez pour peindre !
```

**Avantages** :
- Outil officiel Unity
- Interface professionnelle
- Beaucoup d'options

**Inconvénients** :
- Plus complexe
- Peut être lent sur gros meshes

---

## 🌍 SOLUTION 3 : Unity Terrain

**Si vous convertissez en Terrain Unity** :

### Créer un Terrain

```
Hierarchy → 3D Object → Terrain
```

### Ajouter Details

```
Sélectionnez Terrain
Inspector → Paint Details
Edit Details → Add Detail Mesh
Glissez vos prefabs
Peignez !
```

**Avantages** :
- Système natif Unity
- LOD automatique
- Très performant

**Inconvénients** :
- Doit recréer le terrain
- Perd le mesh actuel

---

## 🐛 DÉPANNAGE

### "Tag 'Foliage' n'existe pas"

**Solution** :
```
Edit → Project Settings → Tags and Layers
Tags → "+" → Ajoutez "Foliage"
```

### "Rien ne se passe quand je clique"

**Vérifications** :
```
[ ] Foliage Painter sélectionné dans Hierarchy
[ ] Vue Scene active (pas Game)
[ ] CTRL enfoncé en cliquant
[ ] Au moins 1 prefab assigné
[ ] Collider sur la surface
```

### "Les plantes flottent dans l'air"

**Solution** :
```
Le mesh doit avoir un Collider !

Sélectionnez fond d'ocean_V1 (1)
Inspector → Add Component → Mesh Collider
```

### "Trop de plantes d'un coup"

**Solution** :
```
Foliage Painter → Density → Réduire à 5-10
```

### "Je veux effacer des plantes"

**Solution** :
```
Option 1 : CTRL + SHIFT + Clic (efface dans la zone)

Option 2 : Mode Erase
  Foliage Painter → Erase Mode: ✓
  CTRL + Clic → Efface
```

---

## 📊 COMPARAISON DES SOLUTIONS

| Solution | Difficulté | Flexibilité | Performance |
|----------|-----------|-------------|-------------|
| **Foliage Painter** | ⭐ Facile | ⭐⭐⭐ | ⭐⭐ |
| **Polybrush** | ⭐⭐ Moyen | ⭐⭐⭐ | ⭐⭐ |
| **Unity Terrain** | ⭐⭐⭐ Difficile | ⭐⭐ | ⭐⭐⭐ |

**Recommandation** : Commencez avec **Foliage Painter** (solution 1) !

---

## 🎊 RÉSUMÉ EXPRESS

**Setup (5 min)** :
1. ✅ Créez le tag "Foliage"
2. ✅ Créez des prefabs de vos plantes
3. ✅ Créez GameObject "Foliage Painter"
4. ✅ Ajoutez le script FoliagePainter
5. ✅ Assignez vos prefabs

**Utilisation (30 sec)** :
```
Sélectionnez Foliage Painter
CTRL + Clic sur la surface
→ Plantes ! 🌿✨
```

**Organisation** :
```
_Foliage (dossier auto-créé)
├─ Coral_16
├─ Coral_16 (1)
├─ Seaweed_03
└─ ... (toutes vos plantes)
```

**Performance** :
- Utilisez LOD Groups sur les prefabs si beaucoup de détails
- Regroupez les plantes similaires
- Utilisez Occlusion Culling

---

## 🚀 PROCHAINES ÉTAPES

1. ✅ Créez le tag "Foliage"
2. ✅ Préparez 5-10 prefabs
3. ✅ Testez avec Foliage Painter
4. ✅ Peignez votre scène sous-marine ! 🌊🌿

**Bon painting ! 🎨**
