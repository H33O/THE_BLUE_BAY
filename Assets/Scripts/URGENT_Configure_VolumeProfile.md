# 🚨 URGENT : Configurer le Volume Profile

## Diagnostic

✅ **Volume Profile existe** : `/Assets/Settings/UnderwaterVolumeProfile.asset`
❌ **Volume Profile VIDE** : Aucun override configuré

**C'est pour ça que rien ne change à l'écran !**

## Solution Immédiate (1 minute)

### Étape 1 : Ouvrir le Volume Profile

1. Dans le **Project**, naviguez vers `/Assets/Settings/`
2. **Cliquez** sur `UnderwaterVolumeProfile`
3. L'Inspector devrait s'ouvrir sur la droite

### Étape 2 : Ajouter les Effets (Overrides)

Dans l'Inspector du Volume Profile, vous devriez voir un bouton **"Add Override"**. Cliquez dessus et ajoutez les 3 effets suivants :

#### A. Color Adjustments (Teinte Bleue)

1. Cliquez **"Add Override"** → **Post-processing** → **Color Adjustments**
2. Vous verrez apparaître une section "Color Adjustments"
3. Configurez :
   - Cochez la case ☑️ à gauche de **"Color Filter"**
   - Cliquez sur la couleur et réglez sur **bleu clair** (exemple : R=102, G=178, B=230)
   - Cochez ☑️ **"Saturation"** → Réglez à **-20**
   - Cochez ☑️ **"Contrast"** → Réglez à **-10**

#### B. White Balance (Ton Froid)

1. Cliquez **"Add Override"** → **Post-processing** → **White Balance**
2. Configurez :
   - Cochez ☑️ **"Temperature"** → Réglez à **-15**
   - Cochez ☑️ **"Tint"** → Réglez à **-5**

#### C. Vignette (Bords Sombres)

1. Cliquez **"Add Override"** → **Post-processing** → **Vignette**
2. Configurez :
   - Cochez ☑️ **"Color"** → Réglez sur **bleu foncé** (exemple : R=0, G=51, B=102)
   - Cochez ☑️ **"Intensity"** → Réglez à **0.35**
   - Cochez ☑️ **"Smoothness"** → Réglez à **0.4**

### Étape 3 : Tester

1. **Sauvegardez** (Ctrl+S)
2. **Lancez le jeu** (Play)
3. Les effets devraient maintenant apparaître !

## Vérification Rapide

### Avant de lancer le jeu, vérifiez :

Dans le Volume Profile `UnderwaterVolumeProfile`, vous devriez voir **3 sections** :
```
✓ Color Adjustments
✓ White Balance  
✓ Vignette
```

Si vous ne voyez RIEN ou que c'est vide, recommencez l'Étape 2.

## Valeurs Exactes pour Copy-Paste

Si vous voulez des valeurs précises :

### Color Adjustments
- **Color Filter** : RGB(0.4, 0.7, 0.9) ou Hex #66B2E6
- **Saturation** : -20
- **Contrast** : -10

### White Balance
- **Temperature** : -15
- **Tint** : -5

### Vignette
- **Color** : RGB(0, 0.2, 0.4) ou Hex #003366
- **Intensity** : 0.35
- **Smoothness** : 0.4

## Résultat Attendu

Une fois configuré :
- Quand vous êtes **sous l'eau** (Y < Water Level) :
  - 🔵 L'écran devient progressivement bleuté
  - 🌊 Les bords s'assombrissent (vignette)
  - 💠 L'atmosphère devient froide et sous-marine
  - 🔄 Une légère distorsion ondulante (si le shader fonctionne)

- Quand vous êtes **au-dessus de l'eau** (Y > Water Level) :
  - ☀️ L'écran redevient normal
  - 🎨 Transition douce en ~1 seconde

## IMPORTANT : Cocher les Cases !

⚠️ **CRITIQUE** : Vous DEVEZ cocher la petite case ☑️ à gauche de chaque paramètre pour qu'il soit actif !

Sans cocher la case, le paramètre n'est pas utilisé, même si vous changez la valeur.

### Example Visuel

```
Color Adjustments
  ☐ Post Exposure          ← PAS COCHÉ = INACTIF
  ☑ Color Filter           ← COCHÉ = ACTIF ✓
  ☐ Hue Shift             ← PAS COCHÉ = INACTIF
  ☑ Saturation -20        ← COCHÉ = ACTIF ✓
  ☑ Contrast -10          ← COCHÉ = ACTIF ✓
```

## Si ça ne fonctionne toujours pas

### Vérifiez dans la Console

Au démarrage du jeu, vous devriez voir :
```
UnderwaterEffectController: Volume Profile 'UnderwaterVolumeProfile' assigné correctement avec 3 effet(s)
```

Si vous voyez :
```
Le Volume Profile est VIDE ! Veuillez ajouter des overrides
```
→ Retournez à l'Étape 2 et ajoutez les overrides.

### Vérifiez le Post-Processing

1. Sélectionnez `/HoverCar/Main Camera` dans la Hierarchy
2. Dans l'Inspector, trouvez **"Universal Additional Camera Data"**
3. Assurez-vous que **"Render Post Processing"** est ☑️ **coché**

### Test Ultime

Pour tester rapidement si le Volume Profile fonctionne :

1. Lancez le jeu
2. Sélectionnez "Underwater Volume" dans la Hierarchy
3. Dans l'Inspector, changez **"Weight"** manuellement à **1**
4. L'écran devrait IMMÉDIATEMENT devenir bleu
5. Si rien ne se passe → Le Volume Profile n'est toujours pas configuré

## Alternative : Utiliser l'Outil Automatique

Si vous ne voulez pas configurer manuellement :

1. **SUPPRIMEZ** le Volume Profile actuel (UnderwaterVolumeProfile.asset)
2. Allez dans **Tools → Setup Underwater System**
3. Cliquez **"Créer et Configurer Automatiquement"**
4. L'outil créera un nouveau profile pré-configuré
5. Testez !

## Résumé

**Cause** : Volume Profile vide (aucun override)
**Solution** : Ajouter 3 overrides (Color Adjustments, White Balance, Vignette)
**Temps** : 1-2 minutes
**Fichier** : `/Assets/Settings/UnderwaterVolumeProfile.asset`

Dès que les overrides sont ajoutés et cochés, les effets fonctionneront immédiatement !
