import React, { useState, useEffect } from "react";
import { Grid, TextField, withStyles, Button } from "@material-ui/core";
import useForm from "./useForm"
import { connect } from 'react-redux';
import * as actions from "../actions/product"
import { useToasts } from "react-toast-notifications";

const styles = theme => ({
    root: {
        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            minWidth: 230,
        }
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 230,
    },
    smMargin: {
        margin: theme.spacing(1)
    },
    mdMargin: {
        margin: theme.spacing(2)
    }
})


const initialFieldValues = {
    name: '',
    quantity: "1"
}


const ProductForm = ({ classes, ...props }) => {
    const { addToast } = useToasts()
    const [submitButtonText, setSubmitButtonText] = useState('ADD')

    const validate = (fieldValues = values) => {
        let temp = { ...errors }
        if ('name' in fieldValues)
            temp.name = fieldValues.name ? "" : "This field is required."
        if ('quantity' in fieldValues)
            temp.quantity = fieldValues.quantity ? "" : "This field is required."
        setErrors({
            ...temp
        })
            return Object.values(temp).every(x => x === "")
    }


    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = useForm(initialFieldValues, validate, props.setCurrentId, setSubmitButtonText)

    const handleSubmit = e => {
        e.preventDefault()
        if (validate()) {
            const onSuccess = () => {
                resetForm()
                addToast("Submitted successfully", { appearance: 'success' })
            }
            if (props.currentId === 0)
                props.createProduct(values, onSuccess)
            else
                props.updateProduct(props.currentId, values, onSuccess)
        }
    }

    useEffect(() => {
        if (props.currentId !== 0) {
            setValues({
                ...props.productList.find(x => x.id === props.currentId)
            })
            setErrors({})
            setSubmitButtonText('Update')
        }
    }, [props.currentId, props.productList, setErrors, setValues])

    useEffect(() => {
        if (props.deletedRecordId !== 0) {
            resetForm()
            props.setDeletedRecordId(0)
        }
    }, [props.deletedRecordId])

    return (
        <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
            <Grid container>
                <Grid item xs={6}>
                    <TextField
                        name="name"
                        variant="outlined"
                        label="Product name"
                        value={values.name}
                        onChange={handleInputChange}
                        {...(errors.name && { error: true, helperText: errors.name })}
                    />
                    <TextField
                        name="quantity"
                        variant="outlined"
                        label="Quantity"
                        value={values.quantity}
                        onChange={handleInputChange}
                        {...(errors.quantity && { error: true, helperText: errors.quantity })}
                        />
                    <div>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            className={classes.mdMargin}
                        >
                            {submitButtonText}
                        </Button>
                        <Button
                            variant="contained"
                            color="default"
                            className={classes.mdMargin}
                            onClick={resetForm}
                        >
                            Reset
                        </Button>
                    </div>
                </Grid>
            </Grid>
        </form>
    );
}

const mapStateToProps = state => {
    return {
        productList: state.product.list
    }
}

const mapActionToProps = {
    createProduct: actions.create,
    updateProduct: actions.update
}


export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(ProductForm));