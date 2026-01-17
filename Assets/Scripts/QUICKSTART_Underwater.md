# 🌊 Guide de Démarrage Rapide - Système Sous-Marin

## ⚡ Configuration en 2 Minutes

### Étape 1 : Générer les Textures de Caustiques
1. Dans Unity, allez dans le menu **Tools → Generate Caustic Textures**
2. Laissez les paramètres par défaut (512px, 8 frames)
3. Cliquez sur **"Générer Textures"**
4. ✅ Les textures sont créées dans `Assets/Textures/Caustics/`

### Étape 2 : Configuration Automatique
1. Allez dans le menu **Tools → Setup Underwater System**
2. Cliquez sur **"Créer et Configurer Automatiquement"** (section Volume Profile)
3. Dans la même fenêtre, assignez les textures :
   - Cliquez sur le petit "+" à côté de "Caustic Textures"
   - Faites glisser les 8 textures de `Assets/Textures/Caustics/` dans la liste
4. Cliquez sur **"Appliquer la Configuration"**
5. ✅ Configuration terminée !

### Étape 3 : Tester
1. Lancez le jeu (Play)
2. Déplacez le HoverCar en haut et en bas
3. Observez :
   - L'écran devient bleu sous l'eau
   - Une distorsion subtile apparaît
   - Les caustiques bougent au fond

## 🎯 Ajustements Rapides

### Le niveau d'eau n'est pas correct ?
1. Sélectionnez `/HoverCar/Main Camera`
2. Dans le composant "Underwater Effect Controller"
3. Changez **"Water Level"** au Y de votre surface d'eau

### La transition est trop rapide/lente ?
1. Sur la caméra, dans "Underwater Effect Controller"
2. Ajustez **"Transition Speed"** (en secondes)
   - Plus petit = plus rapide
   - Plus grand = plus lent

### La distorsion est trop forte ?
1. Sur la caméra, ajustez **"Distortion Intensity"**
   - Recommandé : 0.01 à 0.03
   - Défaut : 0.02

### Les caustiques ne sont pas visibles ?
1. Sélectionnez "Caustic Projector" dans la hiérarchie
2. Vérifiez que les textures sont assignées
3. Augmentez **"Intensity"** dans le composant "Caustic Projector"
4. Vérifiez que le projecteur est au-dessus de votre zone (Y = 10)

## 📋 Checklist de Vérification

- [ ] Les textures de caustiques sont générées
- [ ] Le Volume Profile "UnderwaterVolumeProfile" existe
- [ ] Le Volume Profile est assigné au GameObject "Underwater Volume"
- [ ] Les textures sont assignées au "Caustic Projector"
- [ ] Le "Water Level" correspond à votre surface d'eau
- [ ] Le jeu fonctionne sans erreurs

## 🎨 Améliorations Optionnelles

### Ajouter des Particules (Bulles, Poussière)
1. Créez un GameObject vide : `Clic droit → Create Empty`
2. Nommez-le "Underwater Particles"
3. Ajoutez un Particle System : `Add Component → Particle System`
4. Ajoutez le script : `Add Component → Underwater Particles`
5. Configurez le système de particules :
   - **Start Lifetime** : 3-5
   - **Start Speed** : 0.1-0.5 (montante)
   - **Start Size** : 0.05-0.2
   - **Emission Rate** : 20-50
   - **Shape** : Box (pour remplir la zone visible)
6. Le script activera/désactivera automatiquement les particules

### Limiter les Caustiques à une Zone
**Option 1 : Créer un Layer**
```
1. Project Settings → Tags and Layers
2. Créez un nouveau layer "WaterGround"
3. Assignez ce layer aux objets du fond
4. Sur "Caustic Projector", configurez "Ignore Layers"
```

**Option 2 : Plusieurs Petits Projecteurs**
```
1. Dupliquez "Caustic Projector" (Ctrl+D)
2. Positionnez-le au-dessus d'une zone spécifique
3. Réduisez "Orthographic Size" à 10-20
4. Répétez pour chaque zone
```

### Ajouter du Fog Sous-Marin
1. Dans le "UnderwaterVolumeProfile"
2. Ajoutez un composant "Fog" (si disponible dans URP)
3. Ou utilisez Color Adjustments pour assombrir au loin

## 🔧 Dépannage

### Erreur : "Les effets ne s'activent pas"
**Cause** : Post-processing désactivé
**Solution** :
1. Sélectionnez la caméra
2. Vérifiez le composant "Universal Additional Camera Data"
3. Assurez-vous que "Render Post Processing" est coché

### Erreur : "Les caustiques sont trop grandes"
**Cause** : Orthographic Size trop grand
**Solution** :
1. Sélectionnez "Caustic Projector"
2. Dans le composant "Projector"
3. Réduisez "Orthographic Size" (ex: 20-30)

### Erreur : "La distorsion cause des problèmes visuels"
**Cause** : Intensité trop élevée
**Solution** :
1. Sélectionnez la caméra
2. Réduisez "Distortion Intensity" à 0.01

### Performance : "Le jeu est lent sous l'eau"
**Solutions** :
1. Réduisez le nombre de textures de caustiques (4 au lieu de 8)
2. Réduisez la taille des textures (256 au lieu de 512)
3. Réduisez "Frames Per Second" des caustiques (10 au lieu de 15)
4. Limitez le nombre de projecteurs de caustiques

## 📚 Documentation Complète

Pour plus de détails, consultez :
- **Documentation Bezi** : `/Pages/Système Sous-Marin.md`
- **README détaillé** : `/Assets/Scripts/README_Underwater_System.md`

## 🎮 Profitez de votre monde sous-marin !

Votre système est maintenant prêt. N'hésitez pas à expérimenter avec les paramètres pour obtenir l'effet parfait pour votre jeu !
