pipeline {
    agent any

      environment {
        SONARQUBE_SERVER = 'http://192.168.1.156:9000'  // Update with your SonarQube server URL
        SONARQUBE_TOKEN = credentials('sonartesttoken')  // Use the credentials ID you set for SonarQube token
        SONAR_PROJECT_KEY = 'pipeTest' // Replace with your actual project key
    }
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

        stage('SonarQube Analysis') {
            steps {
                 
                    // Run Sonar analysis using the token
                sh "mvn sonar:sonar -Dsonar.projectKey=${SONAR_PROJECT_KEY} -Dsonar.host.url=${SONARQUBE_SERVER} -Dsonar.login=${SONARQUBE_TOKEN}"
                
            }
        }
    }

   
}
