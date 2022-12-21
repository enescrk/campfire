import React, {useEffect, useState} from 'react';
import "./style.css";
import PageTitle from "../../components/pageTitle";
import UserLine from "../../components/userLine";
import {truthOrDare} from "../../utils/enum";
import "../../components/style.css";
import * as truthQuestionList from "../../utils/truthQuestions.json";
import * as dareQuestionList from "../../utils/dareQuestions.json";
import Counter from "../../components/counter";
import {useContext, UserContext} from "../../utils/contextApi/userContext";
const TruthOrDare = () => {
    const [question, setQuestion] = useState("Söylediğiniz en son yalan nedir ?");
    const [effectedQuestion, setEffectedQuestion] = useState("");
    const limit = 30;
    const [isTargetPerson, setIsTargetPerson] = useState(true);
    const [isOver, setIsOver] = useState(false);
    const [answer, setAnswer] = useState(null);
    const truthQuestions = truthQuestionList.default;
    const dareQuestions = dareQuestionList.default;
    const {users, setUsers} = useContext(UserContext);

    const typingEffect = () => {
        if (question) {
            for (let i = 0; i < question.length; i++) {
                setTimeout(() => {
                    setEffectedQuestion(question.slice(0, i) + question[i])
                }, 80 * i)
            }
        }
    }

    useEffect(() => {
        let tempUser = users;
        tempUser[1].isTurn = true;
        setUsers(tempUser);
    },[])

    useEffect(() => {
        if (answer === truthOrDare.Truth) {
            findTruthQuestion();
        } else {
            findDareQuestion();
        }
    }, [answer])

    useEffect(() => {
        typingEffect();
    }, [question])

    useEffect(() => {
        //TODO: süre bittiğinde puanı silinecek
    },[isOver])

    const findTruthQuestion = () => {
        setQuestion(truthQuestions.filter((q) => q["isUsed"] === false)[0]["question"]);
    }
    const findDareQuestion = () => {
        console.log(dareQuestions)
        setQuestion(dareQuestions.filter((q) => q["isUsed"] === false)[0]["question"]);
    }

    const saveAnswer = (ans) => {
        setAnswer(ans);
    }

    const completeQuestion = () => {
        if (answer === truthOrDare.Truth) {
            truthQuestions.filter((q) => q["question"] === question)[0]["isUsed"] = true;
            findTruthQuestion();
        } else {
            dareQuestions.filter((q) => q["question"] === question)[0]["isUsed"] = true;
            findDareQuestion();
        }
    }

    const passQuestion = () => {
        //TODO: Puan silinecek
        if (answer === truthOrDare.Truth) {
            truthQuestions.filter((q) => q["question"] === question)[0]["isUsed"] = true;
            findTruthQuestion();
        } else {
            dareQuestions.filter((q) => q["question"] === question)[0]["isUsed"] = true;
            findDareQuestion();
        }
    }


    return (<div className={'truthOrDare'}>
            <div className={'mx-auto w-50'}>
                <UserLine/>
                <div className={'mt-4'}>
                    <PageTitle title={'TRUTH OR DARE'}/>
                </div>
            </div>
            {
                (answer === truthOrDare.Truth || answer === truthOrDare.Dare) && (
                    <div className={'mt-5'}>
                        <span className={'question m-2'}>Soru:</span>
                        <span className={'questionText p-2 rounded'}>{effectedQuestion}</span>
                        {
                           <Counter limit={limit} isOver={e => setIsOver(e)} />
                        }


                    </div>
                )
            }
            {
                (isTargetPerson && answer === null) && (
                    <div className="answerContainer mt-5">
                        <div className="truthBox rounded m-3 shadow" onClick={() => saveAnswer(truthOrDare.Truth)}>
                            Truth
                        </div>
                        <div className="dareBox rounded m-3 shadow" onClick={() => saveAnswer(truthOrDare.Dare)}>
                            Dare
                        </div>
                    </div>
                )
            }

            {
                answer != null && (
                    <div className={'mx-auto w-50 mt-4'}>
                        <div className={'description w-100 mx-auto'}><b>Verilen zaman içinde cevap vermelisin. Cevabı
                            verdikten sonra "Tamamladım" butonuna basabilirsin, "Pas
                            Geç" butonuna basarak sonraki soruya geçebilirsin</b></div>
                        <button className={'p4 rounded p-2 w-25 m-1 btn'} onClick={() => completeQuestion()}>Tamamladım
                        </button>
                        <button className={'p5 rounded p-2 w-25 m-1 btn'} onClick={() => passQuestion()}>Pas Geç</button>
                    </div>
                )
            }


        </div>
    )
}

export default TruthOrDare;
