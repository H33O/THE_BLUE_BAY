# Système d'Effets Sous-Marins

Ce système fournit des effets visuels complets pour simuler une expérience sous-marine dans votre jeu.

## Composants Créés

### 1. Scripts
- **UnderwaterEffectController.cs** - Script principal qui détecte la position du joueur et active/désactive les effets
- **CausticProjector.cs** - Gère l'animation et la projection des caustiques

### 2. Shaders
- **UnderwaterDistortion.shader** - Effet de distorsion pour simuler un masque de plongée
- **CausticProjector.shader** - Shader pour projeter les caustiques sur les surfaces

### 3. Materials
- **UnderwaterDistortion.mat** - Material utilisant le shader de distorsion
- **CausticProjector.mat** - Material utilisant le shader de projection de caustiques

### 4. GameObjects dans la Scène
- **Underwater Volume** - Volume de post-processing pour les effets sous-marins
- **Caustic Projector** - Projecteur pour les caustiques au fond de l'eau
- Le composant **UnderwaterEffectController** est attaché à la caméra principale

## Configuration Requise

### Étape 1 : Créer un Volume Profile pour les Effets Sous-Marins

1. Dans Unity, clic droit dans le dossier `/Assets/Settings`
2. Créer → Rendering → URP Volume Profile
3. Nommer le fichier "UnderwaterVolumeProfile"
4. Cliquez sur le profile créé et ajoutez ces effets :

#### Color Adjustments
- **Color Filter** : RGB(0.4, 0.7, 0.9) - Teinte bleue
- **Saturation** : -20 - Désaturation légère
- **Contrast** : -10 - Réduction du contraste

#### White Balance
- **Temperature** : -15 - Température froide
- **Tint** : -5 - Tinte légèrement verte

#### Vignette
- **Color** : RGB(0, 0.2, 0.4) - Bleu foncé
- **Intensity** : 0.35
- **Smoothness** : 0.4

### Étape 2 : Assigner le Volume Profile

1. Sélectionnez le GameObject "Underwater Volume" dans la hiérarchie
2. Dans le composant "Volume", assignez le "UnderwaterVolumeProfile" créé à l'étape 1
3. Assurez-vous que :
   - **Is Global** est activé
   - **Priority** est à 1
   - **Weight** démarre à 0 (sera contrôlé par le script)

### Étape 3 : Configurer les Textures de Caustiques

Pour l'effet caustique, vous avez besoin de textures animées :

1. **Option 1 : Créer vos propres textures**
   - Créez une séquence de 4-8 images de caustiques
   - Importez-les dans `/Assets/Textures/Caustics/`
   - Texture Type : Default
   - Wrap Mode : Repeat

2. **Option 2 : Utiliser des assets gratuits**
   - Cherchez "caustic textures" sur l'Asset Store
   - Ou utilisez des générateurs de caustiques en ligne

3. **Assigner les textures**
   - Sélectionnez "Caustic Projector" dans la hiérarchie
   - Dans le composant "Caustic Projector"
   - Assignez vos textures dans le tableau "Caustic Textures"

### Étape 4 : Ajuster le Niveau d'Eau

1. Sélectionnez la caméra principale `/HoverCar/Main Camera`
2. Dans le composant "Underwater Effect Controller"
3. Ajustez **Water Level** pour qu'il corresponde au niveau Y de votre eau
   - Par exemple, si votre surface d'eau est à Y = 0, laissez à 0
   - Si votre eau est à Y = -5, changez à -5

## Paramètres Ajustables

### UnderwaterEffectController (sur la caméra)

| Paramètre | Description | Valeur Recommandée |
|-----------|-------------|-------------------|
| Water Level | Niveau Y de la surface de l'eau | 0 |
| Transition Speed | Vitesse de transition des effets | 1.0 |
| Distortion Intensity | Force de la distorsion du masque | 0.02 |
| Distortion Speed | Vitesse d'animation de la distorsion | 0.5 |

### CausticProjector

| Paramètre | Description | Valeur Recommandée |
|-----------|-------------|-------------------|
| Frames Per Second | Vitesse d'animation des caustiques | 15 |
| Intensity | Intensité des caustiques | 1.0 |
| Scroll Speed | Vitesse de défilement X et Y | (0.01, 0.01) |

### Projector (sur Caustic Projector)

| Paramètre | Description | Valeur Recommandée |
|-----------|-------------|-------------------|
| Orthographic | Mode de projection | True |
| Orthographic Size | Taille de la zone projetée | 50 |
| Far Clip Plane | Distance maximale de projection | 50 |
| Ignore Layers | Couches à ignorer | UI, TransparentFX |

## Optimisation

### Pour Afficher les Caustiques à Certains Endroits Seulement

1. **Option 1 : Utiliser des Layers**
   - Créez un nouveau layer "Water Ground"
   - Assignez ce layer aux objets au fond de l'eau
   - Dans le Projector, réglez "Ignore Layers" pour ignorer tous les autres layers

2. **Option 2 : Utiliser Plusieurs Projecteurs**
   - Créez plusieurs petits projecteurs
   - Positionnez-les aux endroits spécifiques où vous voulez les caustiques
   - Réduisez leur taille avec "Orthographic Size"

3. **Option 3 : Utiliser un Collider pour Activer/Désactiver**
   - Ajoutez un script qui active/désactive le projecteur
   - Utilisez des triggers pour détecter quand le joueur entre dans une zone

## Test et Débogage

1. **Lancez le jeu**
2. **Déplacez le HoverCar** au-dessus et en dessous du niveau d'eau
3. **Vérifiez que** :
   - L'écran devient bleuté sous l'eau
   - La distorsion apparaît progressivement
   - Les caustiques sont visibles au fond
   - La transition est fluide

## Problèmes Courants

**Les effets ne s'activent pas ?**
- Vérifiez que le Volume Profile est bien assigné
- Vérifiez que "Render Post Processing" est activé sur la caméra (composant Universal Additional Camera Data)

**Les caustiques ne sont pas visibles ?**
- Assurez-vous que les textures sont assignées
- Vérifiez que le projecteur pointe vers le bas (rotation X = 90)
- Ajustez l'intensité dans le material

**La distorsion est trop forte/faible ?**
- Ajustez "Distortion Intensity" dans l'UnderwaterEffectController
- Valeurs recommandées : 0.01 à 0.05

**Performance faible ?**
- Réduisez le nombre de projecteurs
- Réduisez la résolution des textures de caustiques
- Diminuez le nombre de frames dans l'animation des caustiques
