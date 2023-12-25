import React, { useState, useEffect } from "react";

const useForm = (initalFieldValues, validate, setCurrentId, setSubmitButtonText) => {

    const [values, setValues] = useState(initalFieldValues)
    const [errors, setErrors] = useState({})

    const handleInputChange = e => {
        const { name, value } = e.target
        const fieldValue = { [name]: value }
        setValues({
            ...values,
            ...fieldValue
        })
        validate(fieldValue)
    }

    const resetForm = () => {
        setCurrentId(0)
        setValues({
            ...initalFieldValues
        })
        setErrors({})
        setCurrentId(0)
        setSubmitButtonText('ADD')
    }

    return {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    };
}

export default useForm;