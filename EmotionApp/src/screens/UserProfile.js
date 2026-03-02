import React, { useState, useContext } from 'react';
import { View, TextInput, Button, Text, Alert } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { AuthContext } from '../context/AuthContext';

export default function UserProfile() {
  const navigation = useNavigation();


  return (
    <View >
      <View style={styles.navigationContainer}>
        <TouchableOpacity 
                  style={styles.navButton}
                  onPress={() => navigation.navigate('UserHome')}
                >
            <MaterialIcons name="home" size={30} color="grey" />
            <Text>Home</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                  style={styles.loginButton} 
                  
                >
            <MaterialIcons name="person" size={30} color="grey" />
            <Text>Profile</Text>
                </TouchableOpacity>
        
      </View>

      
    </View>
  );
}
