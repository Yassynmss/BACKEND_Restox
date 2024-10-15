pipeline {
    agent any

    stages {
        stage('Checkout Code from GitHub') {
            steps {
                git branch: 'First', url: 'https://github.com/Yassynmss/BACKEND_Restox.git'
            }
        }

        stage('Maven Clean') {
            steps {
                sh 'mvn clean'
            }
        }

        stage('Maven Compile') {
            steps {
                sh 'mvn compile'
            }
        }
    }
}
