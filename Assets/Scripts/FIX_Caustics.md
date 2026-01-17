# 🔧 Correction : Caustics Ne Fonctionnent Pas

## Diagnostic

Le problème des caustics est simple : **aucune texture n'est assignée au Projector**.

Sans textures, le Projector ne peut rien projeter !

## Solution Rapide (2 minutes)

### Méthode 1 : Générer les Textures (Recommandé)

1. **Générer les textures** :
   - Menu **Tools → Generate Caustic Textures**
   - Cliquez **"Générer Textures"**
   - Attendez quelques secondes
   - Les textures seront créées dans `/Assets/Textures/Caustics/`

2. **Assigner les textures** :
   - Sélectionnez **"Caustic Projector"** dans la Hierarchy
   - Dans l'Inspector, trouvez le script **"Caustic Projector"**
   - Développez **"Caustic Textures"** (Array)
   - Changez **Size** à **8**
   - Glissez les 8 textures générées (`caustic_0` à `caustic_7`) dans les slots

3. **Tester** :
   - Lancez le jeu
   - Les caustics devraient maintenant être visibles et animés !

### Méthode 2 : Utiliser l'Outil de Setup

1. **Ouvrir l'outil** :
   - Menu **Tools → Setup Underwater System**

2. **Générer d'abord** :
   - Si pas encore fait : Tools → Generate Caustic Textures

3. **Configurer** :
   - Dans la fenêtre de setup, trouvez **"Caustic Textures"**
   - Développez l'array
   - Assignez les 8 textures

4. **Appliquer** :
   - Cliquez **"Appliquer la Configuration"**

## Vérification

### Après avoir assigné les textures :

1. **Sélectionnez "Caustic Projector"** dans la Hierarchy
2. Dans l'Inspector, vérifiez :
   - ✅ **Projector** component doit être présent
   - ✅ **Material** doit être `CausticProjector.mat`
   - ✅ **Caustic Projector** script doit avoir 8 textures assignées
   - ✅ **Orthographic** doit être coché
   - ✅ **Orthographic Size** entre 20-50

### Pendant le jeu :

- Les caustics devraient être visibles au sol/terrain
- Ils doivent **animer** (changer de frame)
- Ils doivent **scroller** lentement

## Ajustements

Si les caustics sont trop faibles ou invisibles :

### 1. Augmenter l'Intensité

Sélectionnez "Caustic Projector" :
- Script **"Caustic Projector"**
- **Intensity** → Augmentez à 2 ou 3

### 2. Ajuster la Taille

Sélectionnez "Caustic Projector" :
- Component **"Projector"**
- **Orthographic Size** → Augmentez pour couvrir plus de terrain

### 3. Vérifier la Position

Les caustics ne sont visibles que sur les objets dans le cône du Projector :
- Assurez-vous que le Projector est au-dessus du terrain
- Par défaut : Position (0, 10, 0), Rotation (90, 0, 0)

### 4. Vérifier le Layer

Le Projector projette seulement sur certains layers :
- Component **"Projector"**
- **Ignore Layers** → Assurez-vous que votre terrain n'est PAS dans cette liste

## Problèmes Communs

### "Les textures n'existent pas"

→ Vous devez d'abord les générer !
- Tools → Generate Caustic Textures
- Cliquez "Générer Textures"

### "Je ne trouve pas le Caustic Projector"

→ Il n'a pas été créé
- Hierarchy → Clic droit → Create Empty
- Nommez-le "Caustic Projector"
- Ajoutez les components :
  - Component → Rendering → Projector
  - Component → Scripts → Caustic Projector

### "Les caustics ne s'animent pas"

→ Vérifiez dans le script :
- **Frames Per Second** → Doit être > 0 (par défaut 15)
- **Caustic Textures** → Doit contenir plusieurs textures (minimum 2)

### "Les caustics sont trop rapides/lents"

Ajustez :
- **Frames Per Second** : Plus haut = plus rapide
- **Scroll Speed** : Contrôle le défilement

## Alternative : Caustics Simples

Si vous voulez quelque chose de très simple sans textures :

1. Créez une texture procédurale dans le shader
2. OU utilisez une seule texture de bruit
3. OU désactivez complètement les caustics si vous préférez

Pour désactiver :
- Sélectionnez "Caustic Projector"
- Décochez le component en haut à gauche

## Résumé

**Cause** : Textures de caustics non assignées
**Solution** : Générer et assigner 8 textures
**Outils** : 
  - Tools → Generate Caustic Textures
  - Tools → Setup Underwater System
**Temps** : 2 minutes
