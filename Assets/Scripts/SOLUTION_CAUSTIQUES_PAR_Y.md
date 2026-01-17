# 🎯 SOLUTION : Caustiques Uniquement Sous un Niveau Y

## ✅ Problème Résolu !

Votre `fond d'ocean_V1 (1)` a plusieurs materials, et vous voulez que **les caustiques apparaissent uniquement sous un certain niveau Y** (sous l'eau).

### 🔧 Solution Implémentée

**Le shader vérifie maintenant la position Y de chaque pixel** :
- **Au-dessus du Water Level** (Y > 0) → Pas de caustiques
- **Sous le Water Level** (Y < 0) → Caustiques visibles
- **Zone de transition** → Fade progressif (2 mètres par défaut)

---

## 📐 Comment Ça Marche

### Architecture

```
GlobalCausticsController
├── Water Level = 0           ← Niveau de la surface de l'eau
├── Fade Distance = 2         ← Distance de transition
│
└── Shader Global Properties
    ├── _GlobalWaterLevel
    └── _GlobalCausticsFadeDistance

Shader URP_CausticsLit
└── Pour chaque pixel :
    1. Calcule : depthBelowWater = waterLevel - positionY
    2. Si depthBelowWater <= 0 → Pas de caustiques
    3. Sinon → Caustiques avec fade progressif
```

### Calcul dans le Shader

```hlsl
float depthBelowWater = _GlobalWaterLevel - positionWS.y;

if (depthBelowWater <= 0)
{
    return 0; // Au-dessus de l'eau
}

// Fade progressif sur fadeDistance
float depthFade = saturate(depthBelowWater / _GlobalCausticsFadeDistance);

// Caustiques avec intensité progressive
caustics = causticPattern * intensity * depthFade;
```

**Résultat** :
- `Y = 1` (1m au-dessus) → 0% de caustiques
- `Y = 0` (surface) → 0% de caustiques
- `Y = -1` (1m sous l'eau) → 50% de caustiques
- `Y = -2` (2m sous l'eau) → 100% de caustiques
- `Y = -10` (10m sous l'eau) → 100% de caustiques

---

## 🚀 Utilisation Immédiate

### ÉTAPE 1 : Appliquer le Shader à votre Fond d'Océan

Pour que ça fonctionne sur `fond d'ocean_V1 (1)`, vous devez changer le shader de ses materials :

#### Option A : Changer le Shader des Materials Existants

```
1. Hierarchy → fond d'ocean_V1 (1)
2. Inspector → Mesh Renderer → Materials
3. Pour CHAQUE material de la liste :
   a. Cliquez sur le material
   b. Dans l'Inspector du material
   c. Shader → Custom/URP Caustics Lit
```

**IMPORTANT** : Faites ça pour **TOUS** les materials du `fond d'ocean_V1 (1)`

#### Option B : Créer de Nouveaux Materials

```
1. Project → Create → Material → "OceanFloorCaustics"
2. Shader → Custom/URP Caustics Lit
3. Base Color → Ajustez selon vos préférences
4. Hierarchy → fond d'ocean_V1 (1)
5. Assignez le nouveau material
```

### ÉTAPE 2 : Configurer le Niveau d'Eau

Le niveau est déjà configuré à `Y = 0` :

```
Hierarchy → Global Caustics
Inspector :
  - Water Level = 0         ← Surface de l'eau
  - Fade Distance = 2       ← Transition de 2 mètres
```

**Pour changer le niveau d'eau** :
- Si votre eau est à `Y = 5` → Water Level = 5
- Si votre eau est à `Y = -3` → Water Level = -3

### ÉTAPE 3 : Tester

```
1. Play ▶️
2. Déplacez-vous verticalement
3. Observez les caustiques :
   - Au-dessus de Y=0 → Pas de caustiques
   - Sous Y=0 → Caustiques progressives
```

---

## ⚙️ Paramètres

### Sur `/Global Caustics`

| Paramètre | Défaut | Description | Exemples |
|-----------|--------|-------------|----------|
| **Water Level** | 0 | Niveau Y de la surface de l'eau | 0, 5, -10 |
| **Fade Distance** | 2 | Distance de transition (mètres) | 1 (rapide), 5 (lent) |
| **Caustics Intensity** | 0.5 | Force des caustiques | 0.3 à 1.5 |
| **Caustics Speed** | 0.3 | Vitesse d'animation | 0.1 à 0.6 |
| **Caustics Scale** | 1.5 | Taille des motifs | 0.8 à 3.0 |

### Exemples de Configuration

#### Eau à la Surface (Y = 0)
```
Water Level = 0
Fade Distance = 2
```

#### Piscine Surélevée (Y = 10)
```
Water Level = 10
Fade Distance = 3
```

#### Océan Profond (avec transition douce)
```
Water Level = 0
Fade Distance = 5  ← Transition plus douce
```

---

## 🎨 Visualisation du Fade

```
              Y = 2 │           │ Pas de caustiques
                    │           │
              Y = 1 │           │ Pas de caustiques
                    │           │
═════════════ Y = 0 ┼───────────┼ Water Level ⟵ Surface
                    │░░░░░░░░░░░│ 
              Y = -1│░░░░░░░░░░░│ 50% Caustiques (fade)
                    │███████████│
              Y = -2│███████████│ 100% Caustiques ⟵ Fin du fade
                    │███████████│
              Y = -5│███████████│ 100% Caustiques
                    │███████████│
             Y = -10│███████████│ 100% Caustiques
```

**Légende** :
- Blanc = Pas de caustiques
- Gris = Caustiques en transition
- Noir = Caustiques complètes

---

## 🔧 Avantages de Cette Solution

### ✅ Fonctionne avec Plusieurs Materials

**Votre cas** : `fond d'ocean_V1 (1)` a plusieurs materials

**Solution** : Le shader vérifie la **position du pixel**, pas du GameObject entier

**Résultat** :
- Partie au-dessus de Y=0 → Pas de caustiques
- Partie en-dessous de Y=0 → Caustiques
- **Même si c'est le même objet avec plusieurs materials !**

### ✅ Contrôle Précis par Position

```
Objet avec plusieurs materials :
  Material 1 (partie haute Y=5 à Y=2)   → Pas de caustiques
  Material 2 (partie basse Y=2 à Y=-5)  → Caustiques visibles
```

### ✅ Pas Besoin de Volumes

**Vous avez demandé** : "possible avec le underwater volume ou pas ?"

**Réponse** : Non, pas besoin ! Cette solution est **meilleure** :
- Volume = détection de la caméra (tout ou rien)
- Cette solution = détection par pixel (précis)

**Exemple** :
```
Caméra à Y = 10 (au-dessus de l'eau) :
  - Avec Volume → Pas de caustiques nulle part
  - Avec cette solution → Le fond sous-marin (Y < 0) a QUAND MÊME des caustiques ✅
```

### ✅ Transition Douce

Pas de coupure brutale, fade progressif sur `fadeDistance`

---

## 🎯 Cas d'Usage

### 1. Océan avec Fond Visible

```
Configuration :
  Water Level = 0
  Fade Distance = 2
  
Résultat :
  - Surface (Y=0) → Pas de caustiques
  - Fond (Y=-20) → Caustiques complètes
  - Caméra peut être n'importe où
```

### 2. Aquarium avec Eau Surélevée

```
Configuration :
  Water Level = 5     ← Eau à 5m de hauteur
  Fade Distance = 1
  
Résultat :
  - Objets au-dessus de Y=5 → Pas de caustiques
  - Objets sous Y=5 → Caustiques
```

### 3. Grotte Sous-Marine Partiellement Immergée

```
Configuration :
  Water Level = 3
  Fade Distance = 2
  
Résultat :
  - Stalactites (Y > 3) → Sèches, pas de caustiques
  - Stalagmites (Y < 3) → Sous l'eau, avec caustiques
```

---

## 📋 Checklist pour votre Projet

### ✅ Configuration Actuelle

- [x] GlobalCausticsController créé
- [x] Water Level = 0
- [x] Fade Distance = 2
- [x] Shader créé avec vérification Y
- [x] UnderwaterEffectController synchronisé

### ⚠️ À Faire : Appliquer aux Objets

Pour `fond d'ocean_V1 (1)` :

```
[ ] 1. Sélectionner fond d'ocean_V1 (1)
[ ] 2. Noter le nombre de materials
[ ] 3. Pour chaque material :
    [ ] a. Ouvrir le material
    [ ] b. Shader → Custom/URP Caustics Lit
    [ ] c. Vérifier que ça compile
[ ] 4. Tester en Play mode
```

### ✅ Test Final

```
[ ] 1. Play ▶️
[ ] 2. Position caméra Y > 0 → Fond d'océan a des caustiques ✅
[ ] 3. Position caméra Y < 0 → Fond d'océan a des caustiques ✅
[ ] 4. Regarder la zone Y=0 à Y=-2 → Fade visible ✅
```

---

## 🎨 Ajustements Visuels

### Si les Caustiques sont Trop Faibles

```
Global Caustics → Caustics Intensity → 1.0
```

### Si la Transition est Trop Brutale

```
Global Caustics → Fade Distance → 5 (plus doux)
```

### Si les Caustiques Apparaissent Trop Haut

```
Global Caustics → Water Level → -1 (descend le niveau)
```

### Si Vous Voulez des Caustiques Partout (debug)

```
Global Caustics → Water Level → 1000 (très haut)
```

---

## 🐛 Dépannage

### "Je ne vois pas de caustiques sur mon fond d'océan"

**Vérifications** :

1. **Le shader est bien assigné ?**
   ```
   Hierarchy → fond d'ocean_V1 (1)
   Inspector → Materials → Shader = "Custom/URP Caustics Lit"
   ```

2. **Le fond est bien sous le Water Level ?**
   ```
   - Position Y du fond d'océan : ???
   - Water Level : 0
   - Si fond Y > 0 → Pas de caustiques !
   ```

3. **L'intensité est activée ?**
   ```
   Console → "Entré dans l'eau - Caustiques activées"
   Global Caustics → Caustics Intensity > 0
   ```

4. **Play mode est actif ?**
   ```
   Sans Play mode → Pas d'animation/activation
   ```

### "Les caustiques apparaissent au-dessus de l'eau"

**Cause** : Water Level mal configuré

**Solution** :
```
Vérifiez :
  - Position réelle de votre surface d'eau
  - Global Caustics → Water Level = [position de la surface]
```

### "Transition trop brutale"

**Solution** :
```
Global Caustics → Fade Distance → Augmentez (ex: 5)
```

---

## 💡 Astuces Avancées

### Caustiques Seulement dans une Zone

Combinez avec le système d'intensité :

```csharp
// Dans un script custom
if (playerInSpecialZone)
{
    causticsController.SetIntensity(0.8f);
}
else
{
    causticsController.SetIntensity(0f);
}
```

### Plusieurs Niveaux d'Eau

Pour des scènes complexes, vous pouvez :
1. Créer plusieurs materials avec shaders custom
2. Chaque shader avec son propre water level
3. Ou utiliser des propriétés de material différentes

### Debug Visuel

Ajoutez temporairement au shader (dans le fragment) :

```hlsl
// Visualiser le fade
return float4(depthFade, depthFade, depthFade, 1);
```

Résultat :
- Noir = Au-dessus de l'eau
- Gris = Zone de transition
- Blanc = Sous l'eau complètement

---

## 📊 Comparaison des Approches

| Méthode | Notre Solution | Avec Volume | Avec Layers |
|---------|---------------|-------------|-------------|
| **Précision** | ✅ Par pixel | ❌ Par caméra | ⚠️ Par objet |
| **Multi-materials** | ✅ Fonctionne | ❌ Problème | ⚠️ Compliqué |
| **Performance** | ✅ Excellente | ✅ Bonne | ⚠️ Moyenne |
| **Simplicité** | ✅ Simple | ⚠️ Setup complexe | ❌ Très complexe |
| **Fade** | ✅ Oui | ❌ Non | ❌ Non |

**Notre solution est la meilleure pour votre cas !**

---

## 🎊 Résumé

### Ce Qui a Été Fait

1. ✅ Ajout de `waterLevel` et `fadeDistance` au GlobalCausticsController
2. ✅ Shader modifié pour vérifier la position Y de chaque pixel
3. ✅ Calcul de fade progressif sur `fadeDistance`
4. ✅ Synchronisation automatique avec UnderwaterEffectController

### Ce Qu'il Reste à Faire

1. **Appliquer le shader** `Custom/URP Caustics Lit` aux materials de `fond d'ocean_V1 (1)`
2. **Ajuster** Water Level si votre surface d'eau n'est pas à Y=0
3. **Tester** et ajuster les paramètres visuels

### Pourquoi C'est Mieux

- ✅ Fonctionne avec plusieurs materials
- ✅ Précision par pixel
- ✅ Pas besoin de volumes ou layers complexes
- ✅ Contrôle total du niveau Y
- ✅ Fade progressif automatique
- ✅ Performance optimale

**Votre problème est résolu ! Il suffit d'appliquer le shader à vos materials ! 🌊✨**
