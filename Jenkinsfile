pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                // Récupère le code source depuis Git
                git branch: 'First', url: 'https://github.com/Yassynmss/BACKEND_Restox.git'
            }
        }

        stage('Build') {
            steps {
                // Compile et construit le projet avec Maven
                sh 'mvn clean install'
            }
        }

        stage('Test') {
            steps {
                // Exécute les tests unitaires
                sh 'mvn test'
            }
        }

        stage('Deploy') {
            steps {
                // Déploiement ou autre tâche de livraison (à configurer selon ton besoin)
                echo 'Déploiement de l\'application...'
            }
        }
    }

    post {
        always {
            // Nettoie l'environnement à la fin du pipeline
            cleanWs()
        }
        success {
            echo 'Build réussi !'
        }
        failure {
            echo 'Build échoué.'
        }
    }
}
