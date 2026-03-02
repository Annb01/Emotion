import { StyleSheet } from 'react-native';

export default StyleSheet.create({
  startContainer: {
    flex: 1,
    width: '100%',
    height: '100%',
  },
    background: {   
    flex: 1,
    resizeMode: 'cover',
    justifyContent: 'center',
    alignItems: 'center',   
    },
    loginContainer: {
      flex: 1,
      justifyContent: 'flex-start',
        alignItems: 'flex-end',
        right: 20,
        top: 40,
    },
    loginButton: {
        backgroundColor: '#D9D9D9',   
        borderRadius: 28,
        width: 56,
        height: 56,
        justifyContent: 'center',
        alignItems: 'center',

    },
    loginButtonCircle: {
    width: 78,
    height: 78,
    borderRadius: 39,
    backgroundColor: '#D9D9D9',
    justifyContent: 'center',
    alignItems: 'center',
    },
    
    registryContainer: {
      flex: 1,
      justifyContent: 'center',
        alignItems: 'center',
    },
    container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#fff',
    },
    title: {
      fontSize: 24,
      marginBottom: 30,
    },
    input: {
      width: '100%',
      borderWidth: 1,
      borderColor: '#ccc',
      padding: 10,
      marginBottom: 15,
      borderRadius: 5,
    },
    registryContainer: {
      position: "absolute",
      width: '100%',
      height: '150%',
      justifyContent: 'center',
      alignItems: 'center',
    },
    registryButton: {
        backgroundColor: '#D9D9D9',   
        borderRadius: 28,
        width: 200,
        height: 56,
        justifyContent: 'center',
        alignItems: 'center',

    },
    registryButtonCircle: {
    width: 222,
    height: 78,
    borderRadius: 39,
    backgroundColor: '#D9D9D9',
    justifyContent: 'center',
    alignItems: 'center',
    },

    userChouseContainer: {
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-between',
      width: '100%',
      paddingHorizontal: 20,
      marginTop: 40,
  },
    choiceButton: {
      backgroundColor: '#D9D9D9',
      borderRadius: 28,
      width: 140,
      height: 56,
      justifyContent: 'center',
      alignItems: 'center',
  },
    registryPanelContainer: {
      position: 'absolute',
      top: '5%',
      left: '5%',
      right: '5%',
      bottom: '5%',
      justifyContent: 'center',
      borderRadius: 30,
      padding: 20,
      backgroundColor: '#fff',
    },

    backButton: {
    position: 'absolute',     
    left: 10, 
    width: 45,
    height: 45,
    borderRadius: 22.5,
    backgroundColor: '#D9D9D9',
    alignItems: 'center',    
    zIndex: 10,
  },
    registryHeader: {
    width: '100%',
    flexDirection: 'row',
    alignItems: 'center',    
    justifyContent: 'center', 
    height: 60,
    position: 'relative',
    marginTop: 10,
  },
  backButtonText:{
    fontSize: 24,
    color: '#000',
    fontWeight: 'bold',
    justifyContent: 'center',   
  },
  title:{
    fontSize: 20,
    fontWeight: 'bold',
  }
});