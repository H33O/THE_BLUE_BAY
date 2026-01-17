# 🔧 Correction : Système Sous-Marin Ne Fonctionne Pas

## Problème Détecté

Le Volume Profile n'est pas assigné au GameObject "Underwater Volume", ce qui empêche les effets de post-processing de fonctionner.

## Solution en 3 Étapes (2 minutes)

### Étape 1 : Créer le Volume Profile

1. Dans le Project, naviguez vers `/Assets/Settings/`
2. **Clic droit** dans la fenêtre Project
3. **Create → Rendering → URP Volume Profile**
4. Nommez-le **"UnderwaterVolumeProfile"**

### Étape 2 : Configurer le Volume Profile

1. **Sélectionnez** le "UnderwaterVolumeProfile" que vous venez de créer
2. Dans l'Inspector, cliquez sur **"Add Override"**

**Ajoutez et configurez les 3 overrides suivants :**

#### A. Color Adjustments
- Cliquez **"Add Override" → Post-processing → Color Adjustments**
- Cochez **✓ Color Filter** → Réglez sur RGB(102, 178, 230) ou couleur bleutée
- Cochez **✓ Saturation** → Réglez sur **-20**
- Cochez **✓ Contrast** → Réglez sur **-10**

#### B. White Balance
- Cliquez **"Add Override" → Post-processing → White Balance**
- Cochez **✓ Temperature** → Réglez sur **-15**
- Cochez **✓ Tint** → Réglez sur **-5**

#### C. Vignette
- Cliquez **"Add Override" → Post-processing → Vignette**
- Cochez **✓ Color** → Réglez sur RGB(0, 51, 102) ou bleu foncé
- Cochez **✓ Intensity** → Réglez sur **0.35**
- Cochez **✓ Smoothness** → Réglez sur **0.4**

### Étape 3 : Assigner le Volume Profile

1. Dans la **Hierarchy**, sélectionnez le GameObject **"Underwater Volume"**
2. Dans l'**Inspector**, trouvez le composant **"Volume"**
3. Dans le champ **"Profile"**, glissez-déposez le **"UnderwaterVolumeProfile"** que vous avez créé

✅ **C'est fait !** Le système devrait maintenant fonctionner.

## Vérification

### Test Rapide

1. **Lancez le jeu** (Play)
2. Dans la Hierarchy, sélectionnez **"/HoverCar/Main Camera"**
3. Dans l'Inspector, regardez le composant **"Underwater Effect Controller"**
4. Vérifiez que **"Water Level"** est configuré (par défaut = 1)
5. Notez la position Y du HoverCar dans la scène
   - Si Y < Water Level → Vous êtes sous l'eau
   - Si Y > Water Level → Vous êtes au-dessus de l'eau

### Logs dans la Console

Après le Start, vous devriez voir dans la Console :
```
UnderwaterEffectController: Volume Profile assigné correctement
UnderwaterEffectController: Material de distorsion configuré
UnderwaterEffectController initialisé - Water Level: 1, Position initiale joueur: X.XX
```

Si le joueur est sous l'eau au démarrage, vous verrez aussi :
```
Entré dans l'eau
```

### Ajuster le Niveau d'Eau

Si votre HoverCar spawn à Y = 3.61 (comme détecté) :
- Le **Water Level actuel = 1**
- Donc Y (3.61) > Water Level (1) → **Vous êtes AU-DESSUS de l'eau**

**Pour que la voiture soit sous l'eau au spawn :**
1. Sélectionnez **"/HoverCar/Main Camera"**
2. Dans **"Underwater Effect Controller"**
3. Changez **"Water Level"** à **5** (ou plus que 3.61)

**OU** si vous voulez que le niveau d'eau soit à Y = 0 (surface réelle) :
1. Changez **"Water Level"** à **0**
2. La voiture sera considérée sous l'eau seulement si elle descend sous Y = 0

## Valeurs Recommandées

### Configuration Standard (surface d'eau à Y = 0)
- **Water Level** : 0
- La voiture est sous l'eau quand Y < 0
- La voiture est au-dessus de l'eau quand Y > 0

### Valeurs de Test Rapide
Pour tester facilement :
- **Water Level** : 100
- Comme ça, la voiture sera toujours "sous l'eau" et vous verrez immédiatement les effets

## Problèmes Courants

### "Je ne vois toujours rien"

**Vérifiez ces 4 points :**

1. **Volume Profile assigné ?**
   - Hierarchy → "Underwater Volume" → Inspector → "Profile" doit contenir "UnderwaterVolumeProfile"

2. **Post-processing activé ?**
   - Hierarchy → "/HoverCar/Main Camera" → Inspector
   - Component "Universal Additional Camera Data"
   - **✓ Render Post Processing** doit être coché

3. **Water Level correct ?**
   - Si la voiture est à Y = 3.61 et Water Level = 1
   - Alors Y (3.61) > Water Level (1) = PAS sous l'eau
   - Changez Water Level à 5 pour tester

4. **Logs d'erreur ?**
   - Ouvrez la Console (Ctrl+Shift+C)
   - Regardez s'il y a des messages d'erreur en rouge

### "L'écran ne devient pas bleu"

- Le Volume Profile n'est probablement pas assigné
- OU les overrides ne sont pas configurés
- Suivez les étapes 1-3 ci-dessus

### "La distorsion ne fonctionne pas"

- Le matériau `UnderwaterDistortion.mat` est-il assigné ?
- Vérifiez dans "/HoverCar/Main Camera" → "Underwater Effect Controller" → "Underwater Distortion Material"

### "Les caustiques ne sont pas visibles"

Ce problème est séparé du système de base. Les caustiques nécessitent :
1. Des textures assignées au "Caustic Projector"
2. Utilisez : Tools → Generate Caustic Textures
3. Puis assignez les textures générées

## Résumé de la Correction

**Cause du problème** : Volume Profile manquant
**Solution** : Créer et assigner le UnderwaterVolumeProfile
**Temps requis** : 2-3 minutes
**Étapes** : 3 (Créer → Configurer → Assigner)

Une fois corrigé, le système détectera automatiquement quand le HoverCar passe sous le niveau d'eau et activera progressivement les effets.
