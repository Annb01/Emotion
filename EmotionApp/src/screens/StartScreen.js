import React, { useState, useContext } from 'react';
import { View, TextInput, Button, Text, Alert, TouchableOpacity, Image, StyleSheet, ImageBackground} from 'react-native';
import styles from '../style/StartScreen.styles';
import { useNavigation } from '@react-navigation/native';
import { MaterialIcons } from '@expo/vector-icons';
import { Shadow } from 'react-native-shadow-2';
import { AuthContext } from '../context/AuthContext';


export default function StartScreen() {
const [userEmail, setUserEmail] = useState('');
const [userPassword, setUserPassword] = useState('');
const [psychEmail, setPsychEmail] = useState('');
const [psychPassword, setPsychPassword] = useState('');
const [userName, setUserName] = useState('');
const [userSurname, setUserSurname] = useState('');
const [psychName, setPsychName] = useState('');
const [psychSurname, setPsychSurname] = useState('');
const [psychiatryistCode, setPsychiatryistCode] = useState('');
const [psychiatryicRegistryCode, setPsychiatryicRegistryCode] = useState('');
const [email, setEmail] = useState('');
const [password, setPassword] = useState('');

const [modalLoginVisible, setLoginVisible] = useState(false);
const [modalRegistryVisible, setRegistryVisible] = useState(false);
const [modalRegistryUserVisible, setRegistryUserVisible] = useState(false);
const [modalRegistryPsychiatristVisible, setRegistryPsychiatristVisible] = useState(false);

const { onRegisterPsychiatrist } = useContext(AuthContext);
const { onRegisterUser } = useContext(AuthContext);
const { onLogin } = useContext(AuthContext);
  const navigation = useNavigation();


const handleLogin = async () => { 
  try {
    const result = await onLogin(email, password);
    if (result.success) {
      const userRole = result.data.user.roles; 

      Alert.alert('Sukces', 'Zalogowano pomyślnie');

      if (userRole === 'Psychiatrist') {
        navigation.navigate('PsychHome');
      } else {
        navigation.navigate('UserHome');
      }
    } else {
      Alert.alert('Błąd', result.message || 'Nieprawidłowe dane logowania.');
    } 
  } catch (error) {
    console.error(error);
    Alert.alert('Błąd', 'Wystąpił błąd podczas logowania.');
  }
};

const handleRegisterPsych = async () => {
    const result = await onRegisterPsychiatrist(
        psychName, 
        psychSurname, 
        psychEmail, 
        psychPassword, 
        psychiatryicRegistryCode
    );

    if (result.success) {
        alert("Zarejestrowano pomyślnie");
        setModalRegistryPsychiatristVisible(false);
        setRegistryVisible(false);
        setLoginVisible(true);
    } else {
        alert(result.message || "Błąd rejestracji");
    }
};

const handleRegisterUser = async () => {
    const result = await onRegisterUser(
        userName, 
        userSurname, 
        userEmail, 
        userPassword, 
        psychiatryistCode
    );

    if (result.success) {
        alert("Zarejestrowano pomyślnie");
        setModalRegistryUserVisible(false);
        setRegistryVisible(false);
        setLoginVisible(true);
    } else {
        alert(result.message || "Błąd rejestracji");
    }
};

  return (
    <ImageBackground
      source={require('../assets/StartScreen/Start_Background.png')}
      style={styles.background}>
        <View style={styles.startContainer}>
        
          <View style={styles.loginContainer}>
  <View style={styles.loginButtonCircle}>
    <Shadow
        distance={30}
        startColor={'#AEAEC066'}
        offset={[8, 40]}
        containerStyle={styles.shadowWrapper}
      >
        <Shadow
          distance={30}
          startColor={'#FFFFFF'}
          offset={[-5, 17]}
          containerStyle={styles.shadowWrapper}
        >
      </Shadow>
    </Shadow>
    <TouchableOpacity 
          style={styles.loginButton} 
          onPress={() => setLoginVisible(true)}
        >
          <MaterialIcons name="person" size={30} color="grey" />
        </TouchableOpacity>
  </View>
</View>

          <View style={styles.registryContainer}>
  <View style={styles.registryButtonCircle}>
    <Shadow
        distance={30}
        startColor={'#AEAEC066'}
        offset={[8, 40]}
        containerStyle={styles.shadowWrapper}
      >
        <Shadow
          distance={30}
          startColor={'#FFFFFF'}
          offset={[-5, 17]}
          containerStyle={styles.shadowWrapper}
        >
      </Shadow>
    </Shadow>
            <TouchableOpacity style={styles.registryButton} onPress={() => {setRegistryVisible(true); setRegistryUserVisible(true)}}>
                <Text style={styles.buttonText}>Let's start</Text>
            </TouchableOpacity>
          </View>
</View>

          {modalLoginVisible && (
            <View style={styles.registryPanelContainer}>
              <View style={styles.registryHeader}>
                <TouchableOpacity style={styles.backButton}
                  onPress={() => setLoginVisible(false)}>
                  <Text style={styles.backButtonText}>←</Text>
                </TouchableOpacity>
                <Text style={styles.title}>Logowanie</Text>
              </View>

              <TextInput
                style={styles.input}
                placeholder="E-mail"
                value={email}
                onChangeText={setEmail}
              />

              <TextInput
                style={styles.input}
                placeholder="Hasło"
                secureTextEntry
                value={password}
                onChangeText={setPassword}
              />

              <Button title="Zaloguj się" onPress={handleLogin} />
            </View>
          )}
          {modalRegistryVisible && (
            <View style={styles.registryPanelContainer}>
              <View style={styles.registryHeader}>
                <TouchableOpacity style={styles.backButton}
                  onPress={() => setRegistryVisible(false)}>
                  <Text style={styles.backButtonText}>←</Text>
                </TouchableOpacity>
                <Text style={styles.title}>Rejestracja</Text>
              </View>
              
              <View style={styles.userChouseContainer}>
                < TouchableOpacity style={[styles.choiceButton, modalRegistryUserVisible && { opacity: 0.5 }]} 
                onPress={() => {setRegistryUserVisible(true), setRegistryPsychiatristVisible(false);}} 
                disabled={modalRegistryUserVisible}>
                  <Text style={styles.buttonText}>Patient</Text>
              </TouchableOpacity>
                <TouchableOpacity style={[styles.choiceButton, modalRegistryPsychiatristVisible && { opacity: 0.5 }]}
                  onPress={() => {setRegistryPsychiatristVisible(true), setRegistryUserVisible(false);}}
                  disabled={modalRegistryPsychiatristVisible}>
                  <Text style={styles.buttonText}>Psychologist</Text>
              </TouchableOpacity>
                
              </View>
            {modalRegistryUserVisible && (
            <View style={styles.container}>
              <TextInput
                style={styles.input}
                placeholder="Psychiatrist Code"
                value={psychiatryistCode}
                onChangeText={setPsychiatryistCode}
              />

              <TextInput
                style={styles.input}
                placeholder="Name"
                value={userName}
                onChangeText={setUserName}
              />
              <TextInput
                style={styles.input}
                placeholder="Surname"
                value={userSurname}
                onChangeText={setUserSurname}
              />
              <TextInput
                style={styles.input}
                placeholder="E-mail"
                value={userEmail}
                onChangeText={setUserEmail}
              />

              <TextInput
                style={styles.input}
                placeholder="Hasło"
                secureTextEntry
                value={userPassword}
                onChangeText={setUserPassword}
              />

              <Button title="Zaloguj się" onPress={handleRegisterUser} />
              </View>
            )}
              {modalRegistryPsychiatristVisible && (
            <View style={styles.container}>
              <TextInput
                style={styles.input}
                placeholder="Psychiatryic Code"
                value={psychiatryicRegistryCode}
                onChangeText={setPsychiatryicRegistryCode}
              />
              <TextInput
                style={styles.input}
                placeholder="Name"
                value={psychName}
                onChangeText={setPsychName}
              />
              <TextInput
                style={styles.input}
                placeholder="Surname"
                value={psychSurname}
                onChangeText={setPsychSurname}
              />
              <TextInput
                style={styles.input}
                placeholder="E-mail"
                value={psychEmail}
                onChangeText={setPsychEmail}
              />

              <TextInput
                style={styles.input}
                placeholder="Hasło"
                secureTextEntry
                value={psychPassword}
                onChangeText={setPsychPassword}
              />
              
              <Button title="Zaloguj się" onPress={handleRegisterPsych} />
              </View> 
          )}
            </View>
          )}
          
        </View>
    </ImageBackground>
  );
}
